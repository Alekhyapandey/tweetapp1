using com.tweetapp.tweetmicroservice.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.tweetmicroservice.Services
{
    public interface ITweetService
    {
        List<Tweet> GetAllTweets();
        List<Tweet> GetUserTweets(string username);
        Tweet AddTweet(Tweet tweet, string username);
        Tweet UpdateTweet(string tweetId, Tweet tweet);
        bool DeleteTweet(string tweetId);
        Object LikeTweet(string tweetId, string username);
        TweetReply ReplyTweet(string tweetId, string username, TweetReply reply);
    }
}
