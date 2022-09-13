using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.usersmicroservice.Model
{
    public class TweetAppDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string TweetCollectionName { get; set; } = null!;
        public string TweetReplyCollectionName { get; set; } = null!;
        public string TweetLikeCollectionName { get; set; } = null!;
    }
}
