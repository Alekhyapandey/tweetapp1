using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.tweetmicroservice.Model
{
    public class TweetLike
    {
        [Required]
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string TweetLikeId { get; set; } = ObjectId.GenerateNewId().ToString();
        public string TweetId { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
