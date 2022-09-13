using com.tweetapp.tweetmicroservice.Model;
using com.tweetapp.tweetmicroservice.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.tweetmicroservice.Services
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository _tweetRepository;

        public TweetService(ITweetRepository tweetRepository)       //injecting repository dependency
        {
            _tweetRepository = tweetRepository;
        }

        public Tweet AddTweet(Tweet tweet, string username)     //service method to add tweet
        {
            return _tweetRepository.AddTweet(tweet, username);
        }

        public bool DeleteTweet(string tweetId)     //service method to delete tweet
        {
            return _tweetRepository.DeleteTweet(tweetId);
        }

        public List<Tweet> GetAllTweets()       //service method to fetch all tweets
        {
            return _tweetRepository.GetAllTweets();
        }

        public List<Tweet> GetUserTweets(string username)       //service method to fetch user tweets
        {
            return _tweetRepository.GetUserTweets(username);
        }

        public Object LikeTweet(string tweetId,string username)      //service method to like tweet
        {
            return _tweetRepository.LikeTweet(tweetId, username);
        }

        public TweetReply ReplyTweet(string tweetId, string username, TweetReply reply)       //service method to reply tweet or add reply
        {
            return _tweetRepository.ReplyTweet(tweetId, username, reply);
        }

        public Tweet UpdateTweet(string tweetId, Tweet tweet)       //service method to update tweet
        {
            return _tweetRepository.UpdateTweet(tweetId, tweet);
        }
    }
}
