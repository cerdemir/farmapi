using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using farmapi.Context;
using farmapi.Entities;
using farmapi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace farmapi.Services
{
    public class UserService : IUserService
    {
        private FarmApiContext _context;
        private IConfiguration _configuration;

        public UserService(FarmApiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public UserAuthResultModel Authenticate(string username, string password)
        {
            var user = GetUser(username, password);
            if (user == null)
            {
                return new UserAuthResultModel { Authenticated = false };
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY") ?? _configuration.GetConnectionString("SECRET_KEY");;
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new UserAuthResultModel()
            {
                Id = user.Id,
                    Username = user.Username,
                    Token = tokenHandler.WriteToken(token),
                    Authenticated = true
            };
        }

        public User Register(UserRegisterModel registermodel)
        {
            return new User();
        }

        private User GetUser(string username, string password)
        {
            return _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);
        }
    }
}