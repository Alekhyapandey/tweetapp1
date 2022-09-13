using com.tweetapp.authenticationmicroservice.Dtos;
using com.tweetapp.authenticationmicroservice.Model;
using com.tweetapp.authenticationmicroservice.Services;
using MongoDB.Bson;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthUnitTest
{
    internal class AccountServiceTest
    {
        private UserAccount user;
        private AccountDto userDto;
        private User userDetails;
        private AccountResponseDto responseDto;
        string email;
        string password;

        [SetUp]
        public void Setup()
        {
            user = new UserAccount()
            {
                UserAccountId = ObjectId.GenerateNewId().ToString(),
                UserName = "alekhyapandey"
            };

            userDto = new AccountDto()
            {
                Username = "alekyapandey",
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

            responseDto = new AccountResponseDto()
            {
                AccountId = "123456",
                AccountToken = "jwfuiwrhiurthuriehede",
                Username = "alekhyapandey"
            };

            email = "alekhya@mail.com";

            password = "pass123";
        }

        [Test]
        public void TestLoginReturnsObject()
        {
            Mock<IAccountService> mock = new Mock<IAccountService>();
            mock.Setup(m => m.Login(user.UserName, password)).Returns(responseDto);
            AccountResponseDto u = mock.Object.Login(user.UserName, password);
            Assert.AreEqual(u, responseDto);
        }

        [Test]
        public void TestLoginReturnsNull()
        {
            Mock<IAccountService> mock = new Mock<IAccountService>();
            mock.Setup(m => m.Login(user.UserName, password)).Returns(new AccountResponseDto());
            AccountResponseDto u = mock.Object.Login(user.UserName, password);
            Assert.AreNotEqual(u, responseDto);
        }

        [Test]
        public void TestRegisterReturnObject()
        {
            Mock<IAccountService> mock = new Mock<IAccountService>();
            mock.Setup(m => m.Register(userDto)).Returns(responseDto);
            AccountResponseDto u = mock.Object.Register(userDto);
            Assert.AreEqual(u, responseDto);
        }

        [Test]
        public void TestRegisterReturnNull()
        {
            Mock<IAccountService> mock = new Mock<IAccountService>();
            mock.Setup(m => m.Register(userDto)).Returns(new AccountResponseDto());
            AccountResponseDto u = mock.Object.Register(userDto);
            Assert.AreNotEqual(u, responseDto);
        }

        [Test]
        public void TestUpdatePasswordReturnObject()
        {
            Mock<IAccountService > mock = new Mock<IAccountService>();
            mock.Setup(m => m.UpdatePassword(user.UserName, password)).Returns(responseDto);
            AccountResponseDto u = mock.Object.UpdatePassword(user.UserName, password);
            Assert.AreEqual(u, responseDto);
        }

        [Test]
        public void TestUpdatePasswordReturnNull()
        {
            Mock<IAccountService> mock = new Mock<IAccountService>();
            mock.Setup(m => m.UpdatePassword(user.UserName, password)).Returns(new AccountResponseDto());
            AccountResponseDto u = mock.Object.UpdatePassword(user.UserName, password);
            Assert.AreNotEqual(u, responseDto);
        }

        [Test]
        public void TestExistEmailReturnsObject()
        {
            Mock<IAccountService> mock = new Mock<IAccountService>();
            mock.Setup(m => m.ExistsEmail(email)).Returns(userDetails);
            User u = mock.Object.ExistsEmail(email);
            Assert.AreEqual(u, userDetails);
        }

        [Test]
        public void TestExistEmailReturnsNull()
        {
            Mock<IAccountService> mock = new Mock<IAccountService>();
            mock.Setup(m => m.ExistsEmail(email)).Returns(new User());
            User u = mock.Object.ExistsEmail(email);
            Assert.AreNotEqual(u, userDetails);
        }

        [Test]
        public void TestExistsUserReturnsObject()
        {
            Mock<IAccountService> mock = new Mock<IAccountService>();
            mock.Setup(m => m.ExistsUser(userDto.Username)).Returns(user);
            UserAccount u = mock.Object.ExistsUser(userDto.Username);
            Assert.AreEqual(u, user);
        }

        [Test]
        public void TestExistsUserReturnsNull()
        {
            Mock<IAccountService> mock = new Mock<IAccountService>();
            mock.Setup(m => m.ExistsUser(userDto.Username)).Returns(new UserAccount());
            UserAccount u = mock.Object.ExistsUser(userDto.Username);
            Assert.AreNotEqual(u, user);
        }
    }
}
