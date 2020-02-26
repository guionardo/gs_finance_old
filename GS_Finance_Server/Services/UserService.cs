using System;
using System.Security.Cryptography;
using GS_Finance_Server.Interfaces;
using GS_Finance_Server.Interfaces.Repositories;
using GS_Finance_Server.Models;

namespace GS_Finance_Server.Services
{
    public class UserService : IUserService
    {
        private IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
            User user = new User()
            {
                UserName = "guionardo"
            };
            GeneratePassword(user, "1234");
            repository.Update(user);
            user = new User
            {
                UserName = "mari"
            };
            GeneratePassword(user, "1234");
            repository.Update(user);
        }

        public bool Update(User user)
        {
            return _repository.Update(user);
        }

        public bool SetEnabled(User user, bool enabled)
        {
            user.Enabled = enabled;
            return Update(user);
        }

        public bool GeneratePassword(User user, string password)
        {
            user.PasswordHash = GetHashedPassword(password);
            return _repository.Update(user);
        }

        public bool ValidatePassword(User user, string password)
        {
            var hashedPassword = GetHashedPassword(password ?? "");
            return user.PasswordHash.Equals(hashedPassword);
        }

        public User Find(string username)
        {
            return _repository.Find(username);
        }

        private static string GetHashedPassword(string password)
        {
            var sha = new SHA512Managed();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}