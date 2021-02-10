using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Eshop_API.Helpers;
using Eshop_API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Eshop_API.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();

        User GetUser(int id);

        void AddUser(User user);
    }

    public class UserService : IUserService
    {
        private readonly ItemContext _context;

        
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        /*
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test", Role = "admin" }
        };
        */
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, ItemContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            //var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
            var user = _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            // return users without passwords
            return _context.Users.ToList<User>().Select(x => {
                x.Password = null;
                return x;
            });
        }

        public User GetUser(int id)
        {
            //TODO: return users without passwords????
            return _context.Users.Find(id);
        }
        public void AddUser(User user)
        {
            if(_context.Users.Any(u => u.Username == user.Username))
            {
                throw new Exception("Username already exists");
            }
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
