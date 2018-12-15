using System;
using System.Collections.Generic;
using Collabo.API.DTOs;
using Collabo.Common;

namespace Collabo.API.Services{

    public interface ICollaboDataService
    {
        User Register(string firstName, string lastName, string userName, string password, string confirmPassword, string emailID, string secretQuestion, string secretAnswer);
        Session Login(string userName, string password);
        bool TryAddConverserToConversation(Guid user, Guid conversationid, Guid converser, out string message);
        bool TryRemoveConverserFromConversation(Guid user, Guid conversationid, Guid converser, out string message);
        List<Conversation> GetAllConversations();
        Guid CreateConversation(Guid user, Guid channel);
        Guid CreateConversationWithUserSet(Guid user, List<Guid> userSet);
        void CloseConversation(Guid conversation);
        Conversation GetConversation(Guid conversation);
        List<IChannel> GetAllActiveChannelsForUser(Guid user);
        Guid CreateChannel(Guid user, CreateChannelDTO channel);
        bool TryDeleteChannel(Guid user, Guid channel, out string message);
        bool TryAddMemberToChannel(Guid user, Guid channelId, Guid member, out string message);
        bool TryRemoveMemberFromChannel(Guid user, Guid channelId, Guid member, out string message);
        bool Logout(Guid user, Guid iD);
    }
}