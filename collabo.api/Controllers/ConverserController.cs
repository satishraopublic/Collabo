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
    [Route("api/conversation/{conversationid}/converser/")]
    [ApiController]
    public class ConverserController : Controller
    {
        ICollaboDataService _dataService;
        ILoginService _loginService;
        ILogger<ConversationController> _logger;
        public ConverserController(ILogger<ConversationController> logger, ICollaboDataService dataService, ILoginService loginService)
        {
            _dataService = dataService;
            _loginService = loginService;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult AddConverserToConversation(Guid token, Guid conversationid, [FromBody]ConverserDTO converser){
            Session session = _loginService.ValidateUserContext(token);
            string message = null;
            bool created = _dataService.TryAddConverserToConversation(session.User, conversationid, converser.Converser, out message);
            if(created) return Created("", true);
            _logger.LogInformation(message);
            return BadRequest("Cannot add converser to conversation.");
        }

        [HttpDelete]
        public ActionResult RemoveConverserFromConversation(Guid token, Guid conversationid, [FromBody]ConverserDTO converser){
            Session session = _loginService.ValidateUserContext(token);
            string message = null;
            bool removed = _dataService.TryRemoveConverserFromConversation(session.User, conversationid, converser.Converser, out message);
            if(removed) return NoContent();
            _logger.LogInformation(message);
            return BadRequest("Cannot remove converser from conversation.");
        }

    }
}

