using com.tweetapp.authenticationmicroservice.Dtos;
using com.tweetapp.authenticationmicroservice.Model;
using com.tweetapp.authenticationmicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.authenticationmicroservice.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenService _tokenService;

        public AccountService(IAccountRepository accountRepository, ITokenService tokenService)
        {
            _accountRepository = accountRepository;
            _tokenService = tokenService;
        }

        public User ExistsEmail(string email)
        {
            return _accountRepository.ExistEmail(email);
        }

        public UserAccount ExistsUser(string username)
        {
            return _accountRepository.ExistsUser(username);
        }

        public AccountResponseDto Login(string username, string password)
        {
            try
            {
                UserAccount userAccount = _accountRepository.Login(username);    // Retrieving user object

                if (userAccount == null)    // Checking the object is null or not 
                {
                    return null;
                }

                using HMACSHA512 hmac = new HMACSHA512(userAccount.PasswordSalt);    // validating password
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < hash.Length; i++)
                {
                    if (hash[i] != userAccount.PasswordHash[i])
                        return null;
                }

                return new AccountResponseDto    // returning accountResponseDto if the password is valid
                {
                    AccountId = userAccount.UserAccountId,
                    Username = userAccount.UserName,
                    AccountToken = _tokenService.CreateToken(userAccount)
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public AccountResponseDto Register(AccountDto user)
        {
            try
            {
                using HMACSHA512 hmac = new HMACSHA512();

                UserAccount userAccount = new UserAccount
                {
                    UserName = user.Username.ToLower(),
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
                    PasswordSalt = hmac.Key
                };

                bool isSuccess = _accountRepository.Register(userAccount, user);    // Storing the response 

                if (isSuccess)    // If true returning response 
                {
                    return new AccountResponseDto
                    {
                        AccountId = userAccount.UserAccountId,
                        Username = userAccount.UserName,
                        AccountToken = _tokenService.CreateToken(userAccount)
                    };
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }   // If false returning null
        }

        public AccountResponseDto UpdatePassword(string username, string password)
        {
            try
            {
                using HMACSHA512 hmac = new HMACSHA512();

                UserAccount userAccount = new UserAccount
                {
                    UserName = username.ToLower(),
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };

                bool isSuccess = _accountRepository.UpdatePassword(userAccount);    // Storing the response 

                if (isSuccess)    // If true returning response 
                {
                    return new AccountResponseDto
                    {
                        AccountId = userAccount.UserAccountId,
                        Username = userAccount.UserName,
                        AccountToken = _tokenService.CreateToken(userAccount)
                    };
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
