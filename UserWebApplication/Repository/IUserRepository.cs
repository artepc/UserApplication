using Microsoft.AspNetCore.Identity;
using UserWebApplication.Model;

namespace UserWebApplication.Repository
{
    public interface IUserRepository
    {

        public Task<IdentityResult> CreateNewUser(UserModel user);


        public Task<UserModel> GetUserByUsername(string username);


    }
}