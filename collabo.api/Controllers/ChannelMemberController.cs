using System;
using System.Collections.Generic;
using System.Linq;
using Collabo.API.DTOs;
using Collabo.API.Services;
using Collabo.Common;
using Collabo.Common.DTOs;
using Collabo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Collabo.Controllers
{
    [Route("api/channel/{channelid}/member")]
    [ApiController]
    public class ChannelMemberController : Controller
    {
        ICollaboDataService _dataService;
        ILoginService _loginService;
        ILogger<ConversationController> _logger;
        public ChannelMemberController(ILogger<ConversationController> logger, ICollaboDataService dataService, ILoginService loginService)
        {
            _dataService = dataService;
            _loginService = loginService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ListChannelMembers(Guid token, Guid channelId){
            Session session = _loginService.ValidateUserContext(token);
            List<UserViewDTO> users = _dataService.GetAllMembersForChannel(channelId);
            return Ok(users);
        }


        [HttpPost]
        public ActionResult AddMemberToChannel(Guid token, Guid channelId, [FromBody]ChannelMemberDTO channelMember){
            Session session = _loginService.ValidateUserContext(token);
            string message = null;
            bool created = _dataService.TryAddMemberToChannel(session.User, channelId, channelMember.Member, out message);
            if(created) return Created("", true);
            _logger.LogInformation(message);
            return BadRequest("Cannot add member to channel.");
        }


        [HttpDelete]
        public ActionResult RemoveMemberFromChannel(Guid token, Guid channelId, [FromBody]ChannelMemberDTO channelMember){
            Session session = _loginService.ValidateUserContext(token);
            string message = null;
            bool removed = _dataService.TryRemoveMemberFromChannel(session.User, channelId, channelMember.Member, out message);
            if(removed) return NoContent();
            _logger.LogInformation(message);
            return BadRequest("Cannot remove member from channel.");
        }


    }
}