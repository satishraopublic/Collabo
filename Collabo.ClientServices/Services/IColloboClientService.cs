using System;
using System.Collections.Generic;
using Collabo.API.DTOs;
using Collabo.Common.DTOs;

namespace Collabo.ClientServices{
    public interface ICollaboClientService
    {
//New User
        UserViewDTO Register(string firstName, string lastName, string userName, string password, 
                        string confirmPassword, string emailID, string secretQuestion, string secretAnswer);

// Session 
        SessionDTO Login(string userName, string password, out string fixHint);
        bool Logout(Guid sessionId);

//Channels 

        List<ViewChannelDTO> GetAvailableChannels(Guid sessionId);
        bool AddChannel(Guid sessionId, CreateChannelDTO channel);
        bool DeleteChannel(Guid sessionId, Guid channelId);

// Channel Members
        bool AddMemberToChannel(Guid sessionId, Guid channelId, ChannelMemberDTO member);
        bool RemoveMemberFromChannel(Guid sessionId, Guid channelId, ChannelMemberDTO member);

// Conversations
          List<ViewConversationDTO> GetAllConversations(Guid sessionId);
          Guid CreateConversation(Guid sessionId, CreateConversationDTO createInfo);
          bool CloseConversation(Guid sessionId, Guid conversation);


//Converser
        Guid AddConverserToConversation(Guid sessionId, Guid conversationid, ConverserDTO converser);
        bool RemoveConverserFromConversation(Guid sessionId, Guid conversationid, ConverserDTO converser);

    }
}