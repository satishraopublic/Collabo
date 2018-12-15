using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Collabo.Common;
using Collabo.Data;
using Collabo.API.Services;
using Collabo.API.DTOs;

namespace Collabo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : Controller
    {
        ICollaboDataService _dataService;
        ILoginService _loginService;
        ILogger<SessionController> _logger;
        public SessionController(ILogger<SessionController> logger, ICollaboDataService dataService, ILoginService loginService)
        {
            _dataService = dataService;
            _loginService = loginService;
            _logger = logger;
        }
        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            Session uc = _dataService.Login(userName, password);
            if(uc != null){
                _loginService.AddUserContext(uc);
            }
            else{
                _logger.LogError("Login failed. user name or password is incorrect.");
                return BadRequest();
            }                
            _logger.LogInformation($"User {userName} successfully logged in.");
            return Created("",new SessionDTO(uc));
        }

        [HttpDelete]
        public IActionResult Logout(Guid token){
            Session sess = _loginService.ValidateUserContext(token);
            if(_dataService.Logout(sess.User, sess.ID)){
                _loginService.RemoveUserContext(sess);
                return NoContent();
            }
            else{
                return BadRequest();
            }
        }

    }
}