using UserWebApplication.Model;

namespace UserWebApplication.Services
{
    public interface ITokenService
    {
        public string CreateToken(UserModel user);


    }
}