using com.tweetapp.authenticationmicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.authenticationmicroservice.Services
{
    public interface ITokenService
    {
        public string CreateToken(UserAccount userAccount);
    }
}
