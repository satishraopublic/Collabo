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
    [Route("api/channel")]
    [ApiController]
    public class ChannelController : Controller
    {
        ICollaboDataService _dataService;
        ILoginService _loginService;
        ILogger<ConversationController> _logger;
        public ChannelController(ILogger<ConversationController> logger, ICollaboDataService dataService, ILoginService loginService)
        {
            _dataService = dataService;
            _loginService = loginService;
            _logger = logger;
        }

        //TODO: Get all Channels for user - GET
        [HttpGet]
        public ActionResult GetAllChannels(Guid token){
            Session session = _loginService.ValidateUserContext(token);
            List<IChannel> userChannels =_dataService.GetAllActiveChannelsForUser(session.User);
            if(userChannels?.Any() == true){
                return Ok(GetChannelDTO(userChannels));
            }
            return NotFound();
        }

        
        [HttpPost]
        public ActionResult CreateChannel(Guid token, [FromBody]CreateChannelDTO channel){
            Session session = _loginService.ValidateUserContext(token);
            _logger.LogCritical("Session validated.");
            _logger.LogCritical($"Create Channel(Name={channel.Name}, Type={channel.Type}).");
            Guid channelId =_dataService.CreateChannel(session.User, channel);
            if(channelId == Guid.Empty) return BadRequest();
            return Created("https://localhost:5001/api/channel", channelId);
        }

        [HttpDelete]
        public ActionResult DeleteChannel(Guid token, Guid channel){
            string message;
            Session session = _loginService.ValidateUserContext(token);
            if(!_dataService.TryDeleteChannel(session.User, channel, out message)){
                _logger.LogInformation(message);
                return BadRequest("Cannot delete channel.");
            }
            return Ok();
        }
        private List<ViewChannelDTO> GetChannelDTO(List<IChannel> userChannels)
        {
            List<ViewChannelDTO> result = new List<ViewChannelDTO>();
            if(userChannels?.Any() == true){
                foreach(IChannel channel in userChannels){
                    result.Add(new ViewChannelDTO(channel));
                }
            }
            return result;
        }

    }
}
