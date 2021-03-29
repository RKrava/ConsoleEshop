using System;
using System.Collections.Generic;
using System.Text;
using DAL.DataBases;
using DAL.Interfaces;
using DAL.UsersType;

namespace BLL.Users
{
    public class UserService : IUserService
    {
        private readonly IUsersProvider _usersDb;

        public UserService()
        {
            _usersDb = new UsersDb();
        }

        public IUser CurrentUser { get; set; }

        public bool Register(string username, string password)
        {
            return _usersDb.AddUser(username, password);
        }

        public bool TryLogin(string username, string password)
        {
            if (_usersDb.GetUser(username, password) is null) return false;
            CurrentUser = _usersDb.GetUser(username, password);
            return true;
        }

        public IUser GetUser(string username, string password)
        {
            return _usersDb.GetUser(username, password);
        }

        public void Logout()
        {
            CurrentUser = new Guest();
        }

        public List<AuthorizedUser> GetAuthorizedUsers()
        {
            return _usersDb.GetAuthorizedUsers();
        }

        public bool EditUserInfo(int id, string newName, string newSurname)
        {
            var authorizedUsers = GetAuthorizedUsers();
            if (!authorizedUsers.Exists(x => x.Id == id)) return false;
            authorizedUsers.Find(x => x.Id == id).Name = newName;
            authorizedUsers.Find(x => x.Id == id).Surname = newSurname;
            return true;
        }
    }
}
