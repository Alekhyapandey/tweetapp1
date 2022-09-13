using com.tweetapp.authenticationmicroservice.Dtos;
using com.tweetapp.authenticationmicroservice.Model;
using com.tweetapp.authenticationmicroservice.Repository;
using MongoDB.Bson;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthUnitTest
{
    internal class AccountRepositoryTest
    {
        private UserAccount user;
        private AccountDto userDto;
        private User userDetails;
        string email;

        [SetUp]
        public void Setup()
        {
            user = new UserAccount()
            {
                UserAccountId = ObjectId.GenerateNewId().ToString(),
                UserName = "alekhya"
            };

            userDto = new AccountDto()
            {
                Username = "alekhyapandey",
                Password = "alekhya123",
                Email = "alekhya@mail.com"
            };

            userDetails = new User()
            {
                UserId = ObjectId.GenerateNewId().ToString(),
                UserName = "alekhyapandey",
                FirstName = "Alekhya",
                LastName = "pandey",
                Email = "alekhyapandey@mail.com",
                ContactNumber = 1234567809
            };

            email = "alekhya@mail.com";
        }

        [Test]
        public void TestLoginReturnsObject()
        {
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(m => m.Login(user.UserName)).Returns(user);
            UserAccount u = mock.Object.Login(user.UserName);
            Assert.AreEqual(u, user);
        }

        [Test]
        public void TestLoginReturnsNull()
        {
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(m => m.Login(user.UserName)).Returns(new UserAccount());
            UserAccount u = mock.Object.Login(user.UserName);
            Assert.AreNotEqual(u, user);
        }

        [Test]
        public void TestRegisterReturnTrue()
        {
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(m => m.Register(user,userDto)).Returns(true);
            bool u = mock.Object.Register(user, userDto);
            Assert.AreEqual(u, true);
        }

        [Test]
        public void TestRegisterReturnFalse()
        {
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(m => m.Register(user, userDto)).Returns(false);
            bool u = mock.Object.Register(user, userDto);
            Assert.AreEqual(u, false);
        }

        [Test]
        public void TestUpdatePasswordReturnTrue()
        {
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(m => m.UpdatePassword(user)).Returns(true);
            bool u = mock.Object.UpdatePassword(user);
            Assert.AreEqual(u, true);
        }

        [Test]
        public void TestUpdatePasswordReturnFalse()
        {
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(m => m.UpdatePassword(user)).Returns(false);
            bool u = mock.Object.UpdatePassword(user);
            Assert.AreEqual(u, false);
        }

        [Test]
        public void TestExistEmailReturnsObject()
        {
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(m => m.ExistEmail(email)).Returns(userDetails);
            User u = mock.Object.ExistEmail(email);
            Assert.AreEqual(u, userDetails);
        }

        [Test]
        public void TestExistEmailReturnsNull()
        {
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(m => m.ExistEmail(email)).Returns(new User());
            User u = mock.Object.ExistEmail(email);
            Assert.AreNotEqual(u, userDetails);
        }

        [Test]
        public void TestExistsUserReturnsObject()
        {
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(m => m.ExistsUser(userDto.Username)).Returns(user);
            UserAccount u = mock.Object.ExistsUser(userDto.Username);
            Assert.AreEqual(u, user);
        }

        [Test]
        public void TestExistsUserReturnsNull()
        {
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(m => m.ExistsUser(userDto.Username)).Returns(new UserAccount());
            UserAccount u = mock.Object.ExistsUser(userDto.Username);
            Assert.AreNotEqual(u, user);
        }
    }
}
