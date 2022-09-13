using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.tweetmicroservice.Model
{
    public class Tweet
    {
        [Required]
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string TweetId { get; set; } = ObjectId.GenerateNewId().ToString();
        [MaxLength(144)]
        public string TweetText { get; set; } = null!;
        public DateTime CreationTime { get; set; }
        public string UserName { get; set; } = null!;
        [MaxLength(50)]
        public string? Tag { get; set; }
        public List<TweetLike>? TweetLike { get; set; }
        public List<TweetReply>? TweetReply { get; set; }
    }
}
