using System;
using System.Collections.Generic;
using System.Linq;
using GS_Finance_Server.Exceptions;
using GS_Finance_Server.Interfaces.Repositories;
using GS_Finance_Server.Models;

namespace GS_Finance_Server.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private static List<User> users = new List<User>();


        public User Get(int id)
        {
            foreach (var user in users)
                if (user.Id == id)
                    return user;

            throw new ModelNotFoundException($"User #{id} not found");
        }

        public bool Update(User model)
        {
            try
            {
                var user = Get(model.Id);
                user.UserName = model.UserName;
                user.Enabled = model.Enabled;
                user.PasswordHash = model.PasswordHash;
            }
            catch (ModelNotFoundException)
            {
                if (model.Id < 1)
                {
                    model.Id = users.Count + 1;
                }

                users.Add(model);
            }

            return true;
        }

        public bool SetEnabled(User model, bool enabled)
        {
            model.Enabled = enabled;
            return Update(model);
        }

        public User Find(string username)
        {
            foreach (var user in users.Where(user => user.UserName.Equals(username, StringComparison.Ordinal)))
            {
                return user;
            }

            throw new ModelNotFoundException($"User '{username}' not found");
        }
    }
}