using System.Collections.Generic;
using DAL.Interfaces;
using DAL.UsersType;

namespace BLL.Users
{
    public interface IUserService
    {
        IUser CurrentUser { get; set; }
        bool Register(string username, string password);
        bool TryLogin(string username, string password);
        IUser GetUser(string username, string password);
        void Logout();
        List<AuthorizedUser> GetAuthorizedUsers();

        bool EditUserInfo(int id, string newName, string newSurname);
    }
}
