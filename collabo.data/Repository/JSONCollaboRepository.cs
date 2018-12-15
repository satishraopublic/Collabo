using System;
using System.Collections.Generic;
using System.Linq;
using Collabo.Common;
using Collabo.Common.DTOs;

namespace Collabo.Data
{
public class JSONCollaboRepository : ICollaboRepository
{

    CollaboDB _db;
    IDataFileService<CollaboDB> _dataFileService;

    public JSONCollaboRepository(IDataFileService<CollaboDB> dataFileService)
    {
        _dataFileService = dataFileService;
        _db = _dataFileService.GetDB();
    }
    public User Register(string firstName, string lastName, string userName, string password, string confirmPassword, string emailId, string question, string answer)
    {
        Guid newUserId;
        User newUser;
        if(!IsUserAlreadyExists(emailId)){
            newUserId = Guid.NewGuid();
            newUser = new User(){
                 ID = newUserId,
                 FirstName = firstName,
                 LastName = lastName,
                 UserName = userName,
                 Password = password,
                 EmailId = emailId,
                 SecretQuestion = question,
                 SecretAnswer = answer
            };
            _db.Users.Add(newUser);    
            _dataFileService.SaveDB(_db);
            return newUser;
        }       
        return null;
    }


    public Session Login(string userName, string password)
    {
        User loggedUser = _db.Users.FirstOrDefault(c=>string.Equals(c.UserName , userName) 
                                    && string.Equals(c.Password, password, StringComparison.InvariantCultureIgnoreCase));
        if(loggedUser == null) throw new Exception("Access Denied.");
        if(!_db.Sessions.ContainsKey(loggedUser.ID)){
            _db.Sessions.Add(loggedUser.ID, new List<Session>());
        }
        List<Session> sessions = _db.Sessions[loggedUser.ID];
        if(sessions == null) sessions = new List<Session>();
        _db.Sessions[loggedUser.ID] = sessions;
        Session newSession = new Session(loggedUser);
        sessions.Add(newSession);
        _dataFileService.SaveDB(_db);
        return newSession;
    }

    public bool Logout(Guid user, Guid session)
    {
        if(_db.Sessions.ContainsKey(user)){
            Session foundSession = _db.Sessions[user]?.FirstOrDefault(c=>c.ID == session);
            if(foundSession != null){
                foundSession.LoggedOffOn = DateTime.UtcNow;
                _dataFileService.SaveDB(_db);
                return true;
            }
            else{
                throw new Exception($"Invalid session ({session}).");
            }
        }
        else{
            throw new Exception($"Unknown error. Cannot logout session {session}");
        }
    }

    public List<Message> GetAllMessages(Guid session)
    {
        throw new NotImplementedException();
    }
    public Guid SendMessage(Guid session, Guid sendTo, MessageCreateDTO message)
    {
        throw new NotImplementedException();
    }

    public Guid CreateConversation(Guid user, Guid channel)
        {
            IChannel foundChannel = _db.Channels.FirstOrDefault(c=>c.ID == channel && c.Type == ChannelType.Named);
            return CreateConversationForChannel(user, foundChannel);
        }

    public Guid CreateConversationWithUserSet(Guid user, List<Guid> userSet)
    {
        IChannel newUnamedChannel = ChannelFactory.CreateChannel(user);
        if(newUnamedChannel != null){
            newUnamedChannel.AddMemberSet(userSet);
            return CreateConversationForChannel(user, newUnamedChannel);
        }
        return Guid.Empty;
    }

    private Guid CreateConversationForChannel(Guid user, IChannel channel){
            Conversation newConversation;
            if(channel != null){
                newConversation = new Conversation(){
                     ID = Guid.NewGuid(),
                     InitiatedBy = user,
                     InitiatedOn = DateTime.UtcNow,
                     Channel = channel.ID,
                     TerminatedOn = DateTime.MinValue
                };
                if(_db.Conversations == null)_db.Conversations = new List<Conversation>();
                _db.Conversations.Add(newConversation);
                _dataFileService.SaveDB(_db);
                return newConversation.ID;
            }
            return Guid.Empty;
    }

    private bool IsUserAlreadyExists(string emailId){
        return (_db.Users.Any(c=>string.Equals(c.EmailId, emailId, StringComparison.InvariantCultureIgnoreCase)) == true);
    }
    private bool IsSessionAvailable(User loggedUser)
    {
        return _db?.Sessions?.Any(c=>c.Key == loggedUser.ID)==false;
    }

        public Conversation GetConversation(Guid conversation)
        {
            return _db.Conversations.FirstOrDefault(c=>c.ID == conversation);
        }

        public void CloseConversation(Guid conversation)
        {
            Conversation foundConversation = GetConversation(conversation);
            if(foundConversation.TerminatedOn != DateTime.MinValue) throw new Exception("Conversation cannot be closed. It is already closed.");
            foundConversation.TerminatedOn = DateTime.UtcNow;
            _dataFileService.SaveDB(_db);
        }

        public List<Conversation> GetAllConversations()
        {
            return _db.Conversations;
        }

 
        public bool TryAddConverserToConversation(Guid user, Guid conversationid, Guid converser, out string message)
        {
            message = string.Empty;
            Conversation conv = _db.Conversations?.FirstOrDefault(c=> c.ID == conversationid);
            if(conv == null){
                message = $"Conversation {conversationid} not found.";
                return false;
            }
            if(conv.TerminatedOn != DateTime.MinValue){
                message = $"Conversation {conversationid} is closed.";
            }
            IChannel channel = _db.Channels.FirstOrDefault(c=>c.ID == conv.Channel);
            if(channel == null){
                message = $"Channel({conv.Channel}) does not exist or is not associated with conversation {conversationid}.";
                return false;
            }

            if(channel.CurrentMembers.Any(c=>c.Member == user) == false){
                message = $"User {user} is not a member of channel {conv.Channel} of conversations {conversationid}. Adding member {converser} is not allowed.";
                return false;
            }

            if(channel.CurrentMembers.Any(c=>c.Member == converser) == true){
                message = $"User {converser} is already a member of channel {conv.Channel} of conversations {conversationid}.";
                return false;
            }

            channel.AddMember(converser);
            _dataFileService.SaveDB(_db);
            return true;
        }

        public bool TryRemoveConverserFromConversation(Guid user, Guid conversationid, Guid converser, out string message)
        {
            message = string.Empty;
            Conversation conv = _db.Conversations?.FirstOrDefault(c=> c.ID == conversationid);
            if(conv == null){
                message = $"Conversation {conversationid} not found.";
                return false;
            }
            if(conv.TerminatedOn != DateTime.MinValue){
                message = $"Conversation {conversationid} is closed.";
            }
            IChannel channel = _db.Channels.FirstOrDefault(c=>c.ID == conv.Channel);
            if(channel == null){
                message = $"Channel({conv.Channel}) does not exist or is not associated with conversation {conversationid}.";
                return false;
            }

            if(channel.CurrentMembers.Any(c=>c.Member == user) == false){
                message = $"User {user} is not a member of channel {conv.Channel} of conversations {conversationid}. Removing member {converser} is not allowed.";
                return false;
            }

            if(channel.CurrentMembers.Any(c=>c.Member == converser) == false){
                message = $"User {converser} is not a member of channel {conv.Channel} of conversations {conversationid}. User cannot be removed.";
                return false;
            }

            channel.RemoveMember(converser);
            _dataFileService.SaveDB(_db);
            return true;
        }

        public List<IChannel> GetAllActiveChannelsForUser(Guid user)
        {
            return _db.Channels?.Where((c)=>
            {
                if(c.CreatedBy == user) return true;
                if(c.CurrentMembers.Any(d=>d.Member == user)) return true;
                return false;
            })?.ToList();
        }

        public Guid CreateChannel(Guid user, string name, ChannelType type)
        {
         IChannel newNamedChannel = ChannelFactory.CreateChannel(user, type, name);
         _db.Channels.Add(newNamedChannel);
         _dataFileService.SaveDB(_db);
         if(newNamedChannel != null)return newNamedChannel.ID;
         return Guid.Empty;
        }

        public bool TryDeleteChannel(Guid user, Guid channelId, out string message)
        {
            message = string.Empty;
            IChannel channel = _db.Channels.FirstOrDefault(c=>c.ID == channelId && c.CreatedBy == user);
            if(channel == null)  {
                    message = "Channel not found Or channel not created by the deleting user.";
                    return false;
            }
            _db.Channels.Remove(channel);
            _dataFileService.SaveDB(_db);
            return true;
        }

        public bool TryAddMemberToChannel(Guid user, Guid channelId, Guid member, out string message)
        {
            message = string.Empty;
            IChannel channel = _db.Channels.FirstOrDefault(c=>c.ID == channelId);
            if(channel == null){
                message = $"Channel({channelId}) does not exist.";
                return false;
            }

            if(channel.CurrentMembers.Any(c=>c.Member == user) == false){
                message = $"User {user} is not a member of channel {channelId}. Adding member to this channel is not allowed.";
                return false;
            }

            if(channel.CurrentMembers.Any(c=>c.Member == member) == true){
                message = $"User {member} is already a member of channel {channelId}.";
                return false;
            }

            channel.AddMember(member);
            _dataFileService.SaveDB(_db);
            return true;
        }

        public bool TryRemoveMemberFromChannel(Guid user, Guid channelId, Guid member, out string message)
        {
            message = string.Empty;
            IChannel channel = _db.Channels.FirstOrDefault(c=>c.ID == channelId);
            if(channel == null){
                message = $"Channel({channelId}) does not exist.";
                return false;
            }

            if(channel.CurrentMembers.Any(c=>c.Member == user) == false){
                message = $"User {user} is not a member of channel {channelId}. Removing member {member} is not allowed.";
                return false;
            }

            if(channel.CurrentMembers.Any(c=>c.Member == member) == false){
                message = $"User {member} is not a member of channel {channelId}. Member cannot be removed.";
                return false;
            }

            channel.RemoveMember(member);
            _dataFileService.SaveDB(_db);
            return true;
        }
    }
}
