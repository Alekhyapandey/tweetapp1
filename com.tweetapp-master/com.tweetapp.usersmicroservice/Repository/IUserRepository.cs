using com.tweetapp.usersmicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.usersmicroservice.Repository
{
    public interface IUserRepository
    {
        List<User> GetAllUser();
        List<User> SearchUserByName(string username);
    }
}
