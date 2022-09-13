using com.tweetapp.usersmicroservice.Model;
using com.tweetapp.usersmicroservice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.usersmicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Get list of all users
        /// </summary>
        [Route("/api/v1.0/tweets/users/all")]
        [HttpGet]
        public IActionResult GetAllUser()   //action method to fetch all users
        {
            _logger.LogInformation("Inside GetAllUser Action Method...");
            List<User> users = _userService.GetAllUser();
            if(users != null)
            {
                _logger.LogInformation("Succesfully Executed GetAllUser Action Method...");
                return Ok(users);
            }
            else
            {
                _logger.LogError("Error Occured in GetAllUser Action Method...");
                return NotFound();
            }
        }

        /// <summary>
        /// Get list of users by name
        /// </summary>
        [Route("/api/v1.0/tweets/{username}")]
        [HttpGet]
        public IActionResult SearchUsersByName(string username)     //action method to search users by name
        {
            _logger.LogInformation("Inside SearchUsersByName Action Method...");
            List<User> users = _userService.SearchUserByName(username);
            if (users != null)
            {
                _logger.LogInformation("Succesfully Executed SearchUsersByName Action Method...");
                return Ok(users);
            }
            else
            {
                _logger.LogError("Error Occured in SearchUsersByName Action Method...");
                return NotFound();
            }
        }
    }
}
