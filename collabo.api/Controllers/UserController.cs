using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Collabo.Common;
using Collabo.Data;
using Collabo.Common.DTOs;
using Collabo.API.Services;

namespace Collabo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        ICollaboDataService _dataService;
        public UserController(ICollaboDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpPost]
        public IActionResult Register(string firstName, string lastName, string userName, string password, 
                        string confirmPassword, string emailID, string secretQuestion, string secretAnswer)
        {
            User user = _dataService.Register(firstName, lastName, userName, 
                        password, confirmPassword, 
                        emailID, 
                        secretQuestion, secretAnswer);
            if(user == null){
                return BadRequest();
            }                
            return Created("",new UserViewDTO(user));
        }


    }
}