using com.tweetapp.usersmicroservice.Model;
using com.tweetapp.usersmicroservice.Services;
using MongoDB.Bson;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserUnitTest
{
    internal class UserServiceTest
    {
        private User user;
        private List<User> userList;

        [SetUp]
        public void Setup()
        {
            user = new User()
            {
                UserId = ObjectId.GenerateNewId().ToString(),
                UserName = "alekhyapandey",
                FirstName = "alekhya",
                LastName = "pandey",
                Email = "alekhyapandey@gmail.com",
                ContactNumber = 1234567809
            };
            userList = new List<User> { user };
        }

        [Test]
        public void TestSearchUserByNameReturnsObject()
        {
            Mock<IUserService> mock = new Mock<IUserService>();
            mock.Setup(m => m.SearchUserByName(user.UserName)).Returns(userList);
            List<User> u = mock.Object.SearchUserByName(user.UserName);
            Assert.AreEqual(u, userList);
        }

        [Test]
        public void TestSearchUserByNameReturnsNull()
        {
            Mock<IUserService> mock = new Mock<IUserService>();
            mock.Setup(m => m.SearchUserByName(user.UserName)).Returns(new List<User>());
            List<User> u = mock.Object.SearchUserByName(user.UserName);
            Assert.AreNotEqual(u, userList);
        }

        [Test]
        public void TestGetAllUserReturnsObject()
        {
            Mock<IUserService> mock = new Mock<IUserService>();
            mock.Setup(m => m.GetAllUser()).Returns(userList);
            List<User> u = mock.Object.GetAllUser();
            Assert.AreEqual(u, userList);
        }

        [Test]
        public void TestGetAllUserReturnsNull()
        {
            Mock<IUserService> mock = new Mock<IUserService>();
            mock.Setup(m => m.GetAllUser()).Returns(new List<User>());
            List<User> u = mock.Object.GetAllUser();
            Assert.AreNotEqual(u, userList);
        }
    }
}
