using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.authenticationmicroservice.Model
{
    public class TweetAppDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UserAccountCollectionName { get; set; } = null!;
        public string UserCollectionName { get; set; } = null!;
    }
}
