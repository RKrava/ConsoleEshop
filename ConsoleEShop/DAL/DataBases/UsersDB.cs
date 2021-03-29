using System.Collections.Generic;
using DAL.Interfaces;
using DAL.UsersType;

namespace DAL.DataBases
{
    public class UsersDb : IUsersProvider
    {
        private readonly List<string> _usernames;
        private readonly List<string> _passwords;
        private readonly List<IUser>  _users;

        public UsersDb()
        {
            _usernames = new List<string>() {"user","admin"};
            _passwords = new List<string>() {"upass","apass"};
            _users = new List<IUser>() {new AuthorizedUser(), new Admin()};
        }

        public bool AddUser(string username, string password)
        {
            if (_usernames.Contains(username)) return false;
            _usernames.Add(username);
            _passwords.Add(password);
            _users.Add(new AuthorizedUser());
            return true;
        }

        public IUser GetUser(string username, string password)
        {
            if (!_usernames.Contains(username) || !_passwords.Contains(password)) return null;
            IUser user = null;
            var index = _usernames.FindIndex(x => x == username);
            if (_passwords[index] == password) user = _users[index];
            return user;
        }

        public List<AuthorizedUser> GetAuthorizedUsers()
        {
            var authorizedUsers = new List<AuthorizedUser>();
            foreach (var user in _users)
            {
                if (user is AuthorizedUser authorized)
                {
                    authorizedUsers.Add(authorized);
                }
            }

            return authorizedUsers;
        }
    }
}
