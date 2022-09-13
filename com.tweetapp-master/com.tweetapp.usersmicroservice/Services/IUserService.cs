using com.tweetapp.usersmicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.usersmicroservice.Services
{
    public interface IUserService
    {
        List<User> GetAllUser();
        List<User> SearchUserByName(string username);
    }
}
