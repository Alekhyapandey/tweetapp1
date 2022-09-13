using com.tweetapp.authenticationmicroservice.Dtos;
using com.tweetapp.authenticationmicroservice.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.authenticationmicroservice.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ILogger _logger;
        private readonly IMongoCollection<UserAccount> _userAccountCollection;
        private readonly IMongoCollection<User> _userCollection;
        public AccountRepository(IOptions<TweetAppDatabaseSettings> tweetAppDatabaseSettings, ILogger logger)
        {
            _logger = logger;

            MongoClient mongoClient = new MongoClient(
            tweetAppDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                tweetAppDatabaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>(
                tweetAppDatabaseSettings.Value.UserCollectionName);
            _userAccountCollection = mongoDatabase.GetCollection<UserAccount>(
                tweetAppDatabaseSettings.Value.UserAccountCollectionName);
        }
        public User ExistEmail(string email)
        {
            try
            {
                _logger.Debug("Inside ExistEmail Repository Method...");
                User result = _userCollection.Find(x => x.Email == email).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error("Exception Occured in ExistEmail Repository Method...", ex);
                return null;
            }
        }

        public UserAccount ExistsUser(string username)
        {
            try {
                _logger.Debug("Inside ExistsUser Repository Method...");
                UserAccount result = _userAccountCollection.Find(x => x.UserName == username).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error("Exception Occured in ExistsUser Repository Method...", ex);
                return null;
            }
        }

        public UserAccount Login(string username)
        {
            try
            {
                _logger.Debug("Inside Login Repository Method...");
                UserAccount userAccount = _userAccountCollection.Find(x => x.UserName == username).FirstOrDefault();
                return userAccount;
            }
            catch (Exception ex)
            {
                _logger.Error("Exception Occured in Login Repository Method...", ex);
                return null;
            }
        }

        public bool Register(UserAccount userAccount, AccountDto user)
        {
            try
            {
                _logger.Debug("Inside Register Repository Method...");
                _userAccountCollection.InsertOne(userAccount);   // Adding the userAccount to the context
                User newUser = new User()
                {
                    UserId = null,
                    UserName = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    ContactNumber = user.ContactNumber
                };
                _userCollection.InsertOne(newUser);
                _logger.Information("Database Updation Successfull In Register Repository Method...");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Exception Occured in Register Repository Method...", ex);
                return false;
            }
        }

        public bool UpdatePassword(UserAccount userAccount)
        {
            try
            {
                _logger.Debug("Inside UpdatePassword Repository Method...");
                UserAccount account = _userAccountCollection.Find(x => x.UserName == userAccount.UserName).FirstOrDefault();
                account.PasswordHash = userAccount.PasswordHash;
                account.PasswordSalt = userAccount.PasswordSalt;
                _userAccountCollection.ReplaceOne(x=>x.UserAccountId == account.UserAccountId, account);   // Update the userAccount to the context
                _logger.Information("Database Updation Successfull In UpdatePassword Repository Method...");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Exception Occured in UpdatePassword Repository Method...", ex);
                return false;
            }
        }
    }
}
