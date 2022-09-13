using com.tweetapp.authenticationmicroservice.Dtos;
using com.tweetapp.authenticationmicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.authenticationmicroservice.Services
{
    public interface IAccountService
    {
        AccountResponseDto Login(string username, string password);
        AccountResponseDto Register(AccountDto user);
        UserAccount ExistsUser(string username);
        User ExistsEmail(string email);
        AccountResponseDto UpdatePassword(string username, string password);
    }
}
