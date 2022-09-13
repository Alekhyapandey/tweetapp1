using com.tweetapp.tweetmicroservice.Model;
using com.tweetapp.tweetmicroservice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.tweetmicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private readonly ITweetService _tweetService;

        private readonly ILogger<TweetController> _logger;
        public TweetController(ITweetService tweetService , ILogger<TweetController> logger)      //service dependency injection
        {
            _logger = logger;
            _tweetService = tweetService;
        }

        /// <summary>
        /// Get list of all tweets
        /// </summary>
        [Route("/api/v1.0/tweets/all")]
        [HttpGet]
        public IActionResult GetAllTweets()     //action method to fetch all tweets
        {
            _logger.LogInformation("Inside GetAllTweets Action Method...");
            List<Tweet> tweets = _tweetService.GetAllTweets();
            if (tweets != null)
            {
                _logger.LogInformation("Succesfully Executed GetAllTweets Action Method...");
                return Ok(tweets);
            }
            else
            {
                _logger.LogError("Error Occured in GetAllTweets Action Method...");
                return NotFound("No Tweets Found!");
            }
        }

        /// <summary>
        /// Get list of all tweets of a user by username
        /// </summary>
        [Route("/api/v1.0/tweets/{username}")]
        [HttpGet]
        public IActionResult GetUserTweets(string username)     //action method to fetch all tweets of a user
        {
            _logger.LogInformation("Inside GetUserTweets Action Method...");
            List<Tweet> tweets = _tweetService.GetUserTweets(username);
            if (tweets != null)
            {
                _logger.LogInformation("Succesfully Executed GetUserTweets Action Method...");
                return Ok(tweets);
            }
            else
            {
                _logger.LogError("Error Occured in GetUserTweets Action Method...");
                return NotFound("No Tweets Found!");
            }
        }

        /// <summary>
        /// Post method to add a tweet
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "tweetText": "any tweet text",
        ///        "creationTime": "2022-07-30T10:39:08.499Z",
        ///        "userName": "username",
        ///        "tag": "any optional tag"
        ///     }
        ///
        /// </remarks>
        [Route("/api/v1.0/tweets/{username}/add")]
        [HttpPost]
        public IActionResult AddTweet([FromBody]Tweet tweet, string username)   //action method to add tweet
        {
            _logger.LogInformation("Inside AddTweet Action Method...");
            Tweet adddedTweet = _tweetService.AddTweet(tweet, username);
            if (adddedTweet != null)
            {
                _logger.LogInformation("Succesfully Executed AddTweet Action Method...");
                return Ok(adddedTweet);
            }
            else
            {
                _logger.LogError("Error Occured in AddTweet Action Method...");
                return BadRequest("Failed To Add!");
            }
        }

        /// <summary>
        /// Put method to update a tweet
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///        "tweetText": "any updated tweet text",
        ///        "creationTime": "2022-07-30T10:39:08.499Z",
        ///        "userName": "username",
        ///        "tag": "any updated optional tag"
        ///     }
        ///
        /// </remarks>
        [Route("/api/v1.0/tweets/{username}/update/{tweetId}")]
        [HttpPut]
        public IActionResult UpdateTweet(string tweetId, [FromBody] Tweet tweet)    //action method to update tweet
        {
            _logger.LogInformation("Inside UpdateTweet Action Method...");
            Tweet updatedTweet = _tweetService.UpdateTweet(tweetId, tweet);
            if (updatedTweet != null)
            {
                _logger.LogInformation("Succesfully Executed UpdateTweet Action Method...");
                return Ok(updatedTweet);
            }
            else
            {
                _logger.LogError("Error Occured in UpdateTweet Action Method...");
                return BadRequest("Unsuccessfull Updation!");
            }
        }

        /// <summary>
        /// Delete method to delete a tweet
        /// </summary>
        [Route("/api/v1.0/tweets/{username}/delete/{tweetId}")]
        [HttpDelete]
        public IActionResult DeleteTweet(string tweetId)    //action method to delete tweet
        {
            _logger.LogInformation("Inside DeleteTweet Action Method...");
            bool response = _tweetService.DeleteTweet(tweetId);
            if (response)
            {
                _logger.LogInformation("Succesfully Executed DeleteTweet Action Method...");
                return Ok("Deleted Successfully!");
            }
            else
            {
                _logger.LogError("Error Occured in DeleteTweet Action Method...");
                return BadRequest("Nothing to Delete!");
            }
        }

        /// <summary>
        /// Put method to add or remove like to a tweet
        /// </summary>
        [Route("/api/v1.0/tweets/{username}/like/{tweetId}")]
        [HttpPut]
        public IActionResult LikeTweet(string tweetId, string username)      //action method to add like to tweet
        {
            _logger.LogInformation("Inside LikeTweet Action Method...");
            object? likedTweet = _tweetService.LikeTweet(tweetId, username);
            if (likedTweet is Tweet)
            {
                _logger.LogInformation("Succesfully Executed LikeTweet Action Method...");
                return Ok("Like Added!");
            }
            else if(likedTweet is TweetLike)
            {
                _logger.LogInformation("Succesfully Executed LikeTweet Action Method...");
                return Ok("Like Removed!");
            }
            else
            {
                _logger.LogError("Error Occured in LikeTweet Action Method...");
                return BadRequest("Unsuccessfull Updation!");
            }
        }

        /// <summary>
        /// Post method to add reply to a tweet
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///        "replyText": "any reply text",
        ///        "creationTime": "2022-07-30T11:00:16.375Z",
        ///        "tweetId": "tweetid from url",
        ///        "userName": "username from url",
        ///        "tag": "any optional tag"
        ///     }
        ///
        /// </remarks>
        [Route("/api/v1.0/tweets/{username}/reply/{tweetId}")]
        [HttpPost]
        public IActionResult ReplyTweet(string tweetId, string username, [FromBody]TweetReply reply)       //action method to add reply
        {
            _logger.LogInformation("Inside ReplyTweet Action Method...");
            TweetReply replyTweet = _tweetService.ReplyTweet(tweetId, username, reply);
            if (replyTweet != null)
            {
                _logger.LogInformation("Succesfully Executed ReplyTweet Action Method...");
                return Ok(replyTweet);
            }
            else
            {
                _logger.LogError("Error Occured in ReplyTweet Action Method...");
                return BadRequest("Failed To Add!");
            }
        }
    }
}
