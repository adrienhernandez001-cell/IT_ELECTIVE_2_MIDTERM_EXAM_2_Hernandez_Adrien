using System;
using ClinicPatientVisitMonitoringSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClinicPatientVisitMonitoringSystem.Repositories
{
    public class UserRepository
    {
        private static readonly List<User> Users = new List<User>();

        public UserRepository()
        {
        }

        public User? GetByUsername(string username)
        {
            return Users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public User? GetById(int id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }

        public bool UsernameExists(string username)
        {
            return Users.Any(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public void Add(User user)
        {
            user.Id = Users.Count == 0 ? 1 : Users.Max(u => u.Id) + 1;
            Users.Add(user);
        }
    }
}