using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.authenticationmicroservice.Dtos
{
    public class AccountResponseDto
    {
        public string AccountId { get; set; } = null!;
        public string AccountToken { get; set; } = null!;
        public string Username { get; set; } = null!;
    }
}
