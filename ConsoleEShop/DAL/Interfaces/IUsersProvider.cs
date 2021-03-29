using DAL.UsersType;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IUsersProvider
    {
        bool AddUser(string username, string password);
        IUser GetUser(string username, string password);
        List<AuthorizedUser> GetAuthorizedUsers();
    }
}
