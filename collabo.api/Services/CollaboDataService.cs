using System;
using System.Collections.Generic;
using Collabo.API.DTOs;
using Collabo.Common;
using Collabo.Data;
using Microsoft.Extensions.Logging;

namespace Collabo.API.Services{
    public class CollaboDataService : ICollaboDataService
    {
        ICollaboRepository _repo;
        ILogger<CollaboDataService> _logger;
        public CollaboDataService(ILogger<CollaboDataService> logger, ICollaboRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }
        public void CloseConversation(Guid conversation)
        {
            _repo.CloseConversation(conversation);
        }

        public Guid CreateChannel(Guid user, CreateChannelDTO channel)
        {
            return _repo.CreateChannel(user, channel.Name, channel.Type);
        }

        public Guid CreateConversation(Guid user, Guid channel)
        {
            return _repo.CreateConversation(user, channel);
        }

        public Guid CreateConversationWithUserSet(Guid user, List<Guid> userSet)
        {
            return _repo.CreateConversationWithUserSet(user, userSet);
        }

        public bool TryDeleteChannel(Guid user, Guid channel, out string message)
        {
            return _repo.TryDeleteChannel(user, channel, out message);
        }

        public List<IChannel> GetAllActiveChannelsForUser(Guid user)
        {
            return _repo.GetAllActiveChannelsForUser(user);
        }

        public List<Conversation> GetAllConversations()
        {
            return _repo.GetAllConversations();
        }

        public Conversation GetConversation(Guid conversation)
        {
            return _repo.GetConversation(conversation);
        }

        public Session Login(string userName, string password)
        {
            return _repo.Login(userName, password);
        }

        public User Register(string firstName, string lastName, string userName, string password, string confirmPassword, string emailID, string secretQuestion, string secretAnswer)
        {
            return _repo.Register(firstName, lastName, userName, password, confirmPassword, emailID, secretQuestion, secretAnswer);
        }

        public bool TryAddConverserToConversation(Guid user, Guid conversationid, Guid converser, out string message)
        {
            return _repo.TryAddConverserToConversation(user, conversationid, converser, out message);
        }

        public bool TryRemoveConverserFromConversation(Guid user, Guid conversationid, Guid converser, out string message)
        {
            return _repo.TryRemoveConverserFromConversation(user, conversationid, converser, out message);
        }

        public bool TryAddMemberToChannel(Guid user, Guid channelId, Guid member, out string message)
        {
            return _repo.TryAddMemberToChannel(user, channelId, member, out message);
        }

        public bool TryRemoveMemberFromChannel(Guid user, Guid channelId, Guid member, out string message)
        {
            return _repo.TryRemoveMemberFromChannel(user, channelId, member, out message);
        }

        public bool Logout(Guid user, Guid session)
        {
            try{
                return _repo.Logout(user, session);
            }
            catch(Exception ex){
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}