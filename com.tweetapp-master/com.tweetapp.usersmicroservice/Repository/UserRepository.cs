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

namespace com.tweetapp.usersmicroservice.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;
        private readonly ILogger _logger;

        public UserRepository(IOptions<TweetAppDatabaseSettings> tweetAppDatabaseSettings, ILogger logger)     //db connection dependency injection
        {
            _logger = logger;

            MongoClient mongoClient = new MongoClient(
            tweetAppDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                tweetAppDatabaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>(
                tweetAppDatabaseSettings.Value.UserCollectionName);
        }

        public List<User> GetAllUser()  //repository method to fetch all users
        {
            try
            {
                _logger.Debug("Inside GetAllUser Repository Method...");
                return _userCollection.Find(FilterDefinition<User>.Empty).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("Exception Occured in GetAllUser Repository Method...", ex);
                return null;
            }
        }

        public List<User> SearchUserByName(string username)     //repository method to search by username
        {
            try
            {
                _logger.Debug("Inside SearchUserByName Repository Method...");
                BsonRegularExpression queryExpr = new BsonRegularExpression(new Regex(username, RegexOptions.None));
                FilterDefinitionBuilder<User> builder = Builders<User>.Filter;
                FilterDefinition<User> filter = builder.Regex("UserName", queryExpr);       //using regex and creating filter to search by partial username
                return _userCollection.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("Exception Occured in SearchUserByName Repository Method...", ex);
                return null;
            }
        }
    }
}
