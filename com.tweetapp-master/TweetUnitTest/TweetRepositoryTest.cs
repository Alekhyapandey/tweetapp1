using com.tweetapp.tweetmicroservice.Model;
using com.tweetapp.tweetmicroservice.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TweetUnitTest
{
    internal class TweetRepositoryTest
    {
        private Tweet tweet;
        private TweetReply reply;
        private List<Tweet> tweetList;

        [SetUp]
        public void Setup()
        {
            tweet = new Tweet()
            {
                TweetId = ObjectId.GenerateNewId().ToString(),
                TweetText = "",
                CreationTime = new DateTime(),
                UserName = "alekhya",
                Tag = "",
                TweetLike = new List<TweetLike>(),
                TweetReply = new List<TweetReply>()
            };
            reply = new TweetReply()
            {
                TweetReplyId = ObjectId.GenerateNewId().ToString(),
                TweetId = "",
                ReplyText = "",
                Tag = "",
                UserName = "alekhya",
                CreationTime = new DateTime()
            };
            tweetList = new List<Tweet> { tweet};
        }

        [Test]
        public void TestAddTweetReturnsObject()
        {
            Mock<ITweetRepository> mock = new Mock<ITweetRepository>();
            mock.Setup(m => m.AddTweet(tweet,tweet.UserName)).Returns(tweet);
            Tweet t = mock.Object.AddTweet(tweet, tweet.UserName);
            Assert.AreEqual(t, tweet);
        }

        [Test]
        public void TestAddTweetReturnsNull()
        {
            Mock<ITweetRepository> mock = new Mock<ITweetRepository>();
            mock.Setup(m => m.AddTweet(tweet, tweet.UserName)).Returns(new Tweet());
            Tweet t = mock.Object.AddTweet(tweet, tweet.UserName);
            Assert.AreNotEqual(t, tweet);
        }

        [Test]
        public void TestDeleteTweetReturnsTrue()
        {
            Mock<ITweetRepository> mock = new Mock<ITweetRepository>();
            mock.Setup(m => m.DeleteTweet(tweet.TweetId)).Returns(true);
            Object t = mock.Object.DeleteTweet(tweet.TweetId);
            Assert.AreEqual(t, true);
        }

        [Test]
        public void TestDeleteTweetReturnsFalse()
        {
            Mock<ITweetRepository> mock = new Mock<ITweetRepository>();
            mock.Setup(m => m.DeleteTweet(tweet.TweetId)).Returns(false);
            Object t = mock.Object.DeleteTweet(tweet.TweetId);
            Assert.AreNotEqual(t, true);
        }

        [Test]
        public void TestGetAllTweetsReturnsObject()
        {
            Mock<ITweetRepository> mock = new Mock<ITweetRepository>();
            mock.Setup(m => m.GetAllTweets()).Returns(tweetList);
            List<Tweet> t = mock.Object.GetAllTweets();
            Assert.AreEqual(t, tweetList);
        }

        [Test]
        public void TestGetAllTweetsReturnsNull()
        {
            Mock<ITweetRepository> mock = new Mock<ITweetRepository>();
            mock.Setup(m => m.GetAllTweets()).Returns(new List<Tweet>());
            List<Tweet> t = mock.Object.GetAllTweets();
            Assert.AreNotEqual(t, tweetList);
        }

        [Test]
        public void TestGetUserTweetsReturnsObject()
        {
            Mock<ITweetRepository> mock = new Mock<ITweetRepository>();
            mock.Setup(m => m.GetUserTweets(tweet.UserName)).Returns(tweetList);
            List<Tweet> t = mock.Object.GetUserTweets(tweet.UserName);
            Assert.AreEqual(t, tweetList);
        }

        [Test]
        public void TestGetUserTweetsReturnsNull()
        {
            Mock<ITweetRepository> mock = new Mock<ITweetRepository>();
            mock.Setup(m => m.GetUserTweets(tweet.UserName)).Returns(new List<Tweet>());
            List<Tweet> t = mock.Object.GetUserTweets(tweet.UserName);
            Assert.AreNotEqual(t, tweetList);
        }

        [Test]
        public void TestUpdateTweetReturnsObject()
        {
            Mock<ITweetRepository> mock = new Mock<ITweetRepository>();
            mock.Setup(m => m.UpdateTweet(tweet.TweetId,tweet)).Returns(tweet);
            Tweet t = mock.Object.UpdateTweet(tweet.TweetId, tweet);
            Assert.AreEqual(t, tweet);
        }

        [Test]
        public void TestUpdateTweetReturnsNull()
        {
            Mock<ITweetRepository> mock = new Mock<ITweetRepository>();
            mock.Setup(m => m.UpdateTweet(tweet.UserName,tweet)).Returns(new Tweet());
            Tweet t = mock.Object.UpdateTweet(tweet.UserName, tweet);
            Assert.AreNotEqual(t, tweet);
        }

        [Test]
        public void TestLikeTweetReturnsObject()
        {
            Mock<ITweetRepository> mock = new Mock<ITweetRepository>();
            mock.Setup(m => m.LikeTweet(tweet.TweetId, tweet.UserName)).Returns(tweet);
            Object t = mock.Object.LikeTweet(tweet.TweetId, tweet.UserName);
            Assert.AreEqual(t, tweet);
        }

        [Test]
        public void TestLikeTweetReturnsNull()
        {
            Mock<ITweetRepository> mock = new Mock<ITweetRepository>();
            mock.Setup(m => m.LikeTweet(tweet.UserName, tweet.UserName)).Returns(new Tweet());
            Object t = mock.Object.LikeTweet(tweet.UserName, tweet.UserName);
            Assert.AreNotEqual(t, tweet);
        }

        [Test]
        public void TestReplyTweetReturnsObject()
        {
            Mock<ITweetRepository> mock = new Mock<ITweetRepository>();
            mock.Setup(m => m.ReplyTweet(tweet.TweetId, tweet.UserName, reply)).Returns(reply);
            TweetReply t = mock.Object.ReplyTweet(tweet.TweetId, tweet.UserName, reply);
            Assert.AreEqual(t, reply);
        }

        [Test]
        public void TestReplyTweetReturnsNull()
        {
            Mock<ITweetRepository> mock = new Mock<ITweetRepository>();
            mock.Setup(m => m.ReplyTweet(tweet.TweetId, tweet.UserName, reply)).Returns(new TweetReply());
            TweetReply t = mock.Object.ReplyTweet(tweet.TweetId, tweet.UserName, reply);
            Assert.AreNotEqual(t, reply);
        }
    }
}
