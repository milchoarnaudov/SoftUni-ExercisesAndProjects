﻿using SharedTrip.Data;
using SharedTrip.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SharedTrip.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext context;

        public UsersService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void CreateUser(string username, string email, string password)
        {
            var hashedPassword = ComputeHash(password);
            var user = new User { Username = username, Email = email, Password = hashedPassword };

            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var hashedPassword = ComputeHash(password);
            return this.context.Users.FirstOrDefault(x => x.Username == username && x.Password == hashedPassword)?.Id;
        }

        public bool IsEmailAvailable(string email)
        {
            return !this.context.Users.Any(x => x.Email == email);
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this.context.Users.Any(x => x.Username == username);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
