using com.tweetapp.usersmicroservice.Model;
using com.tweetapp.usersmicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.usersmicroservice.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;   //injecting repository
        }

        public List<User> GetAllUser()  //service method to fetch all users
        {
            return _userRepository.GetAllUser();
        }

        public List<User> SearchUserByName(string username)     //service method to fetch user details
        {
            return _userRepository.SearchUserByName(username);
        }
    }
}
