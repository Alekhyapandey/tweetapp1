using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.authenticationmicroservice.Dtos
{
    public class Account
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
