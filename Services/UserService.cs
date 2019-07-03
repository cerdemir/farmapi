using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
            var salt = CreateRandomSalt();
            var hashedPassword = CreateHash(salt, registermodel.Password);
            var user = new User()
            {
                Name = registermodel.Name,
                Username = registermodel.Username,
                Password = hashedPassword,
                Salt = salt
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        private string CreateHash(string salt, string password)
        {
            var byteSalt = Convert.FromBase64String(salt);
            var hasher = new Rfc2898DeriveBytes(password, byteSalt, 1000);
            var ret = hasher.GetBytes(20);
            return Convert.ToBase64String(ret);
        }

        private string CreateRandomSalt()
        {
            var saltGeneraqtor = new RNGCryptoServiceProvider();
            var salt = new byte[16];
            saltGeneraqtor.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
        private User GetUser(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == username);
            var hashedPassword = CreateHash(user.Salt, password);
            if (user == null)
            {
                return null;
            }

            var isPassOK = hashedPassword == user.Password;

            if (isPassOK)
            {
                return user;
            }
            else
            {
                return null;
            }

        }
    }
}