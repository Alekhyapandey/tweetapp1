using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.tweetmicroservice.Model
{
    public class TweetReply
    {
        [Required]
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string TweetReplyId { get; set; } = ObjectId.GenerateNewId().ToString();
        [MaxLength(144)]
        public string ReplyText { get; set; } = null!;
        public DateTime CreationTime { get; set; }
        public string TweetId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        [MaxLength(50)]
        public string? Tag { get; set; }
    }
}
