using farmapi.Models;

namespace farmapi.Services
{
    public interface IUserService
    {
        UserAuthResultModel Authenticate(string username, string password);
        Entities.User Register(UserRegisterModel registermodel);
    }
}