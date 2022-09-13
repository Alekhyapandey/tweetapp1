using com.tweetapp.authenticationmicroservice.Dtos;
using com.tweetapp.authenticationmicroservice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.authenticationmicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService , ILogger<AccountController> logger)    // Assigning the accountService with the help of constructor
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Post method for Login
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "username": "username",
        ///        "password": "password"
        ///     }
        ///
        /// </remarks>
        [Route("/api/v1.0/tweets/login")]
        [HttpPost]
        public ActionResult<AccountResponseDto> Login(Account account)
        {
            _logger.LogInformation("Inside Login Action Method...");
            string username = account.Username, password = account.Password;    // Assigning username and password from accountDto parameter

            if (username == null && password == null)    // Validating username and password
                return BadRequest("Username and password are required");

            if (username == null)
                return BadRequest("Username is required");

            if (password == null)
                return BadRequest("Password is required");

            AccountResponseDto user = _accountService.Login(username, password);    // Calling accountService.Login with username and password

            if (user == null)    // Checking the response(user) null or not, if null returning unauthorized
                return Unauthorized("Invalid username or password");

            _logger.LogInformation("Succesfully Executed Login Action Method...");
            return new AccountResponseDto    // If the response(user) is not null then return AccountResponseDto
            {
                AccountId = user.AccountId,
                Username = user.Username,
                AccountToken = user.AccountToken
            };
        }

        /// <summary>
        /// Post method to register
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "username": "username",
        ///        "password": "password",
        ///        "email": "email@email.com",
        ///        "firstName": "Firstname",
        ///        "lastName": "Lastname",
        ///        "contactNumber": 7826541237
        ///     }
        ///
        /// </remarks>
        [Route("/api/v1.0/tweets/register")]
        [HttpPost]
        public ActionResult<AccountResponseDto> Register(AccountDto accountDto)
        {
            _logger.LogInformation("Inside Register Action Method...");
            string username = accountDto.Username, password = accountDto.Password;    // Assigning username and password from accountDto parameter

            if (username == null && password == null)    // Validating username and password
                return BadRequest("Username and password are required");

            if (username == null)
                return BadRequest("Username is required");

            if (password == null)
                return BadRequest("Password is required");

            if (_accountService.ExistsUser(username) != null)    // Checking user exist or not with the provided username. If exist returning BadRequest
            {
                return BadRequest("Username is taken");
            }

            if (_accountService.ExistsEmail(accountDto.Email) !=null)    // Checking email exist or not with the provided email. If exist returning BadRequest
            {
                return BadRequest("Email is taken");
            }

            AccountResponseDto user = _accountService.Register(accountDto);    // If the username is the unique one, saving response to user

            if (user == null)    // Checking the response(user) null or not, if null returning BadRequest
                return BadRequest("Unable to register...Try again!");

            _logger.LogInformation("Succesfully Executed Register Action Method...");
            return new AccountResponseDto    // If the response(user) is not null then return AccountResponseDto
            {
                AccountId = user.AccountId,
                AccountToken = user.AccountToken,
                Username = user.Username
            };
        }

        /// <summary>
        /// Put method to change password
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "username": "username from url",
        ///        "password": "password"
        ///     }
        ///
        /// </remarks>
        [Route("/api/v1.0/tweets/{username}/forgot")]
        [HttpPut]
        public ActionResult<AccountResponseDto> UpdatePassword(Account account, string username)
        {
            _logger.LogInformation("Inside UpdatePassword Action Method...");

            string newPassword = account.Password;
            if (username == null || newPassword == null)
            {
                return BadRequest("Username and Password is required");
            }

            if (_accountService.ExistsUser(username) == null)    // Checking user exist or not with the provided username. If exist returning BadRequest
            {
                return BadRequest("Check Username");
            }

            AccountResponseDto user = _accountService.UpdatePassword(username, newPassword);

            if (user == null)    // Checking the response(user) null or not, if null returning BadRequest
                return BadRequest("Unable to update...Try again!");

            _logger.LogInformation("Succesfully Executed UpdatePassword Action Method...");
            return new AccountResponseDto    // If the response(user) is not null then return AccountResponseDto
            {
                AccountId = user.AccountId,
                AccountToken = user.AccountToken,
                Username = user.Username
            };
        }
    }
}
