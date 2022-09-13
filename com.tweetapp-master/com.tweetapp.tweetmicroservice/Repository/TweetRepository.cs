using com.tweetapp.tweetmicroservice.Model;
using com.tweetapp.usersmicroservice.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace com.tweetapp.tweetmicroservice.Repository
{
    public class TweetRepository : ITweetRepository
    {
        private readonly ILogger _logger;
        private readonly IMongoCollection<Tweet> _tweetCollection;
        private readonly IMongoCollection<TweetReply> _tweetReplyCollection;
        private readonly IMongoCollection<TweetLike> _tweetLikeCollection;

        public TweetRepository(IOptions<TweetAppDatabaseSettings> tweetAppDatabaseSettings, ILogger logger)     //db connection dependency injection
        {
            _logger = logger;

            MongoClient mongoClient = new MongoClient(
            tweetAppDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                tweetAppDatabaseSettings.Value.DatabaseName);

            _tweetCollection = mongoDatabase.GetCollection<Tweet>(
                tweetAppDatabaseSettings.Value.TweetCollectionName);
            _tweetReplyCollection = mongoDatabase.GetCollection<TweetReply>(
                tweetAppDatabaseSettings.Value.TweetReplyCollectionName);
            _tweetLikeCollection = mongoDatabase.GetCollection<TweetLike>(
                tweetAppDatabaseSettings.Value.TweetLikeCollectionName);
        }

        public Tweet AddTweet(Tweet tweet, string username)     //repository method to add tweet
        {
            try
            {
                _logger.Debug("Inside AddTweet Repository Method...");
                Tweet tweetObj = _tweetCollection.Find(x => x.TweetId == tweet.TweetId).FirstOrDefault();
                if (tweetObj == null)
                {
                    tweet.UserName = username;
                    _tweetCollection.InsertOne(tweet);
                }
                else
                {
                    _tweetCollection.ReplaceOne(x => x.TweetId == tweet.TweetId, tweet);
                }
                _logger.Information("Database Updation Successfull In AddTweet Repository Method...");
                return tweet;
            }
            catch (Exception ex)
            {
                _logger.Error("Exception Occured in AddTweet Repository Method...", ex);
                return null;
            }
        }

        public bool DeleteTweet(string tweetId)     //repository method to delete tweet
        {
            try
            {
                _logger.Debug("Inside DeleteTweet Repository Method...");
                Tweet tweet = _tweetCollection.Find(x => x.TweetId == tweetId).FirstOrDefault();
                if (tweet == null)
                    return false;
                _tweetCollection.DeleteOne(x => x.TweetId == tweetId);
                _logger.Information("Database Updation Successfull In DeleteTweet Repository Method...");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Exception Occured in DeleteTweet Repository Method...", ex);
                return false;
            }
        }

        public List<Tweet> GetAllTweets()   //repository method to fetch all tweets
        {
            try
            {
                _logger.Debug("Inside GetAllTweets Repository Method...");
                return _tweetCollection.Find(FilterDefinition<Tweet>.Empty).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("Exception Occured in GetAllTweets Repository Method...", ex);
                return null;
            }
        }

        public List<Tweet> GetUserTweets(string username)   //repository method to fetch tweet by username
        {
            try
            {
                _logger.Debug("Inside GetUserTweets Repository Method...");
                return _tweetCollection.Find(x=>x.UserName==username).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("Exception Occured in GetUserTweets Repository Method...", ex);
                return null;
            }
        }

        public Tweet ReplaceTweet(string tweetId)    //helper method to replace tweet
        {
            Tweet tweet = _tweetCollection.Find(x => x.TweetId == tweetId).FirstOrDefault();
            tweet.TweetLike = _tweetLikeCollection.Find(x => x.TweetId == tweet.TweetId).ToList();
            _tweetCollection.ReplaceOne(x => x.TweetId == tweet.TweetId, tweet);
            return tweet;
        }

        public Object LikeTweet(string tweetId, string username)  //repository method to like tweet
        {
            try
            {
                _logger.Debug("Inside LikeTweet Repository Method...");
                TweetLike tweetLikeObj = _tweetLikeCollection.Find(x => x.TweetId == tweetId && x.UserName==username).FirstOrDefault();
                if (tweetLikeObj == null)
                {
                    TweetLike tweetLike = new TweetLike
                    {
                        UserName = username,
                        TweetId = tweetId
                    };
                    _tweetLikeCollection.InsertOne(tweetLike);
                    _logger.Information("Database Updation Successfull In LikeTweet Repository Method...");
                    return ReplaceTweet(tweetId);
                }
                else
                {
                    _tweetLikeCollection.DeleteOne(x => x.TweetId == tweetId && x.UserName == username);
                    ReplaceTweet(tweetId);
                    _logger.Information("Database Updation Successfull In LikeTweet Repository Method...");
                    return tweetLikeObj;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Exception Occured in LikeTweet Repository Method...", ex);
                return null;
            }
        }

        public TweetReply ReplyTweet(string tweetId, string username, TweetReply reply)    //repository method to add reply
        {
            try
            {
                _logger.Debug("Inside ReplyTweet Repository Method...");
                reply.UserName = username;
                reply.TweetId = tweetId;
                _tweetReplyCollection.InsertOne(reply);
                Tweet tweet = _tweetCollection.Find(x => x.TweetId == tweetId).FirstOrDefault();
                tweet.TweetReply = _tweetReplyCollection.Find(x => x.TweetId == tweet.TweetId).ToList();
                _tweetCollection.ReplaceOne(x => x.TweetId == tweet.TweetId, tweet);
                _logger.Information("Database Updation Successfull In ReplyTweet Repository Method...");
                return reply;
            }
            catch (Exception ex)
            {
                _logger.Error("Exception Occured in ReplyTweet Repository Method...", ex);
                return null;
            }
        }

        public Tweet UpdateTweet(string tweetId, Tweet tweet)    //repository method to update tweet
        {
            try
            {
                _logger.Debug("Inside UpdateTweet Repository Method...");
                Tweet tweetObj = _tweetCollection.Find(x => x.TweetId == tweetId).FirstOrDefault();
                if (tweetObj == null)
                {
                    return null;
                }
                tweet.TweetId = tweetObj.TweetId;
                _tweetCollection.ReplaceOne(x => x.TweetId == tweet.TweetId, tweet);
                _logger.Information("Database Updation Successfull In UpdateTweet Repository Method...");
                return tweet;
            }
            catch (Exception ex)
            {
                _logger.Error("Exception Occured in UpdateTweet Repository Method...", ex);
                return null;
            }
        }
    }
}
