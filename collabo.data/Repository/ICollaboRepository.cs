using System;
using System.Collections.Generic;
using Collabo.Common;
using Collabo.Common.DTOs;

namespace Collabo.Data
{
public interface  ICollaboRepository{

//USER
    User Register(string firstName, string lastName, string userName, string password, string confirmPassword, string emailId, string question, string answer);

//SESSION
    Session Login(string userName, string password);
    bool Logout(Guid user, Guid session);

//Message
    List<Message> GetAllMessages(Guid session);
    Guid SendMessage(Guid session, Guid sendTo, MessageCreateDTO message);

// Conversations
    Guid CreateConversation(Guid user, Guid channel);
    Guid CreateConversationWithUserSet(Guid user, List<Guid> userSet);
    Conversation GetConversation(Guid conversation);
        Guid CreateChannel(Guid user, string name, ChannelType type);
        void CloseConversation(Guid conversation);
    List<Conversation> GetAllConversations();
    bool TryAddConverserToConversation(Guid user, Guid conversationid, Guid converser, out string message);
    bool TryRemoveConverserFromConversation(Guid user, Guid conversationid, Guid converser, out string message);
        List<IChannel> GetAllActiveChannelsForUser(Guid user);
        bool TryDeleteChannel(Guid user, Guid channelId, out string message);
        bool TryAddMemberToChannel(Guid user, Guid channelId, Guid member, out string message);
        bool TryRemoveMemberFromChannel(Guid user, Guid channelId, Guid member, out string message);
    }
}