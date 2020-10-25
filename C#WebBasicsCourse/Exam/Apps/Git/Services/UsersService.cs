using Git.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Git.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext dbContext;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string CreateUser(string username, string email, string password)
        {
            var hashedPassword = ComputeHash(password);

            var user = new User
            {
                Email = email,
                Username = username,
                Password = hashedPassword
            };

            var userEntity = this.dbContext.Users.Add(user);
            this.dbContext.SaveChanges();

            return userEntity?.Entity?.Id;
        }

        public string GetUserId(string username, string password)
        {
            var hashedPassword = ComputeHash(password);
            var user = this.dbContext.Users.FirstOrDefault(x => x.Username == username && x.Password == hashedPassword);
            return user?.Id;
        }

        public bool IsEmailAvailable(string email)
        {
            return !this.dbContext.Users.Any(x => x.Email == email);
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this.dbContext.Users.Any(x => x.Username == username);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
