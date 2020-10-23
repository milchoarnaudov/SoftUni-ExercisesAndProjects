using SULS.Services;
using SULS.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace SULS.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersServce;

        public UsersController(IUsersService usersServce)
        {
            this.usersServce = usersServce;
        }

        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        public HttpResponse Logout()
        {
            if (this.IsUserSignedIn())
            {
                this.SignOut();
            }

            return this.Redirect("/");
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            var userId = this.usersServce.GetUserId(username, password);

            if(userId == null)
            {
                return this.Error("Invalid Username or Password");
            }

            this.SignIn(userId);
            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputViewModel input)
        {
            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Confirm Password and Password are not matching");
            }

            if (input.Username.Length < 5 || input.Username.Length > 20 || string.IsNullOrEmpty(input.Username))
            {
                return this.Error("Username must be longer than 4 and shorter than 21");
            }

            if (string.IsNullOrEmpty(input.Email) || !new EmailAddressAttribute().IsValid(input.Email))
            {
                return this.Error("Invalid email address.");
            }

            if (input.Password.Length < 6 || input.Password.Length > 20)
            {
                return this.Error("Password must be longer than 6 and shorter than 21");
            }

            if (!usersServce.IsEmailAvailable(input.Email))
            {
                return this.Error("Email is already taken");
            }

            if (!usersServce.IsUsernameAvailable(input.Username))
            {
                return this.Error("Username is already taken");
            }

            this.usersServce.CreateUser(input.Username, input.Email, input.Password);
            return this.Redirect("/Users/Login");
        }
    }
}
