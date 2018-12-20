using System;
using System.Net;
using System.Collections.Generic;
using Collabo.API.DTOs;
using Collabo.Common.DTOs;
using RestSharp;


namespace Collabo.ClientServices{
    public class CollaboClientService : ICollaboClientService
    {
        private static RestClient _client;
        private static string _serviceURL;
        public void Configure(string serviceURL, bool overridePreviousConfiguration=false){
            if(_client == null || overridePreviousConfiguration){
                _serviceURL = serviceURL;
                _client = new RestClient(_serviceURL);
            }
            else{
                throw new Exception("Service already initialized.");
            }
        }

            // var request = new RestRequest("api/session?username={username}&password={password}", Method.POST);
            // request.AddUrlSegment("username", "srao2");
            // request.AddUrlSegment("password", "password");

            // // client.Execute<SessionDTO>(request, response => {
            // //     SessionDTO dto = response.Data;
            // //     Console.WriteLine($"Session ID={dto.ID}");
            // // });

            // var response2 = client.Execute<SessionDTO>(request);
            // Console.WriteLine($" Data = {response2?.Data}");
            // var id = response2.Data.ID;
            // Console.WriteLine($"Session id = {id}");
            // Console.Read();
        public static CollaboClientService INSTANCE = new CollaboClientService();

        private CollaboClientService()
        {
            
        }
        public SessionDTO Login(string userName, string password,out string fixHint)
        {
            SessionDTO result=null;
            fixHint = string.Empty;
            ThrowIfNotConfigured();
            var request = new RestRequest("api/session?username={username}&password={password}", Method.POST);
            request.AddUrlSegment("username", userName);
            request.AddUrlSegment("password", password);
            var response2 = _client.Execute<SessionDTO>(request);
            if(ProcessResponse(response2, out fixHint)){
                result = response2.Data;
            }
            return result;
        }


        public bool Logout(Guid sessionId)
        {
            throw new NotImplementedException();
        }
        public bool AddChannel(Guid sessionId, CreateChannelDTO channel)
        {
            throw new NotImplementedException();
        }


        public Guid AddConverserToConversation(Guid sessionId, Guid conversationid, ConverserDTO converser)
        {
            throw new NotImplementedException();
        }

        public bool AddMemberToChannel(Guid sessionId, Guid channelId, ChannelMemberDTO member)
        {
            throw new NotImplementedException();
        }

        public bool CloseConversation(Guid sessionId, Guid conversation)
        {
            throw new NotImplementedException();
        }

        public Guid CreateConversation(Guid sessionId, CreateConversationDTO createInfo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteChannel(Guid sessionId, Guid channelId)
        {
            throw new NotImplementedException();
        }

        public List<ViewConversationDTO> GetAllConversations(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public List<ViewChannelDTO> GetAvailableChannels(Guid sessionId)
        {
            throw new NotImplementedException();
        }


        public UserViewDTO Register(string firstName, string lastName, string userName, string password, string confirmPassword, string emailID, string secretQuestion, string secretAnswer)
        {
            throw new NotImplementedException();
        }

        public bool RemoveConverserFromConversation(Guid sessionId, Guid conversationid, ConverserDTO converser)
        {
            throw new NotImplementedException();
        }

        public bool RemoveMemberFromChannel(Guid sessionId, Guid channelId, ChannelMemberDTO member)
        {
            throw new NotImplementedException();
        }
 
 //PRIVATE methods
         private void ThrowIfNotConfigured()
        {
            if(_client == null) throw new Exception("Service is not configured.");
        }

         private void ThrowIfNoResponse(object response)
        {
            if(response == null) throw new Exception("Service is unable to process the request at this time.");
        }

         private bool ProcessResponse(IRestResponse response, out string hint)
        {
            hint = string.Empty;
            ThrowIfNoResponse(response);
            switch(response.StatusCode){
                case HttpStatusCode.BadRequest:
                    hint = response.StatusDescription;
                    return false;
                case HttpStatusCode.InternalServerError:
                    throw new Exception(response.StatusDescription);
            }
            return true;
        }

   }
}