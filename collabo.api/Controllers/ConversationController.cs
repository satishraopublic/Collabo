using System;
using System.Collections.Generic;
using System.Linq;
using Collabo.API.DTOs;
using Collabo.API.Services;
using Collabo.Common;
using Collabo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Collabo.Controllers
{
    [Route("api/conversation")]
    [ApiController]
    public class ConversationController : Controller
    {
        ICollaboDataService _dataService;
        ILoginService _loginService;
        ILogger<ConversationController> _logger;
        public ConversationController(ILogger<ConversationController> logger, ICollaboDataService dataService, ILoginService loginService)
        {
            _dataService = dataService;
            _loginService = loginService;
            _logger = logger;
        }

        
        [HttpGet]
        public ActionResult GetAllConversationsForUser(Guid token){
            Session session =  _loginService.ValidateUserContext(token);
            List<Conversation> conversations = _dataService.GetAllConversations()?.Where(c=>c.InitiatedBy == session.User && c.TerminatedOn == DateTime.MinValue)?.ToList();
            if(conversations?.Any() == false) return NotFound("No conversations found for user.");
            return Ok(GetConversationDTOs(conversations));
        }

        [HttpPost]
        public ActionResult CreateConversation(Guid token, [FromBody]CreateConversationDTO createInfo){
               Session session =  _loginService.ValidateUserContext(token);
                Guid conversation;
               if(createInfo.Channel != Guid.Empty)
               {
                    conversation = _dataService.CreateConversation(session.User, createInfo.Channel);
               }
               else if(createInfo.UserSet.Any()==true)
               {
                    conversation = _dataService.CreateConversationWithUserSet(session.User, createInfo.UserSet);
               }
               else
               {
                   return BadRequest("Users or Channel not available.");
               }
               if(conversation == Guid.Empty) return BadRequest("Unable to create required channel.");
            return Created("", conversation);
        }

        [HttpDelete]
        public ActionResult CloseConversation(Guid token, Guid conversation){
            Session session =  _loginService.ValidateUserContext(token);
            if(!IsValidConversation(conversation)
                || !IsOpenConversation(conversation)) 
                return BadRequest("Conversation not found.");
            _dataService.CloseConversation(conversation);
            return Ok("Conversation closed.");
        }

        private bool IsValidConversation(Guid conversation)
        {
            Conversation foundConversation = _dataService.GetConversation(conversation);
            return foundConversation != null;
        }
        private bool IsOpenConversation(Guid conversation)
        {
            Conversation foundConversation = _dataService.GetConversation(conversation);
            if(foundConversation != null && foundConversation.TerminatedOn != DateTime.MinValue) return true;
            return false;
        }
        private List<ViewConversationDTO> GetConversationDTOs(List<Conversation> conversations)
        {
            List<ViewConversationDTO> result=new List<ViewConversationDTO>(); 
            if(conversations?.Any()==true){
                    conversations.ForEach((c)=>{
                        result.Add(new ViewConversationDTO(c));
                    });
            }
            return result;
        }

    }
}
