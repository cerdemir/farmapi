using farmapi.Models;

namespace farmapi.Services
{
    public interface IUserService
    {
        AuthResultModel Authenticate(string username, string password);
    }
}