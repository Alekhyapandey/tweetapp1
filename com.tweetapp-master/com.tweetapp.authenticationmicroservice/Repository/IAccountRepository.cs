using com.tweetapp.authenticationmicroservice.Dtos;
using com.tweetapp.authenticationmicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.authenticationmicroservice.Repository
{
    public interface IAccountRepository
    {
        UserAccount Login(string username);
        bool Register(UserAccount userAccount, AccountDto user);
        UserAccount ExistsUser(string username);
        User ExistEmail(string email);
        bool UpdatePassword(UserAccount userAccount);
    }
}
