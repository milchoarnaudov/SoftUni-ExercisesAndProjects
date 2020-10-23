using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface IUsersService
    {
        void CreateUser(string username, string email, string password);

        string GetUserId(string username, string password);

        bool IsEmailAvailable(string email);

        bool IsUsernameAvailable(string username);
    }
}
