using DAL.Enums;
using DAL.Interfaces;

namespace DAL.UsersType
{
    public class AuthorizedUser : IUser
    {
        private static int _number;
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public AuthorizedUser()
        {
            Id = ++_number;
            Name = "UserName";
            Surname = "UserSurname";
            Rights = Rights.AuthorizedUser;
        }

        public void SetNewInfo(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Surname: {Surname}";
        }

        public Rights Rights { get; set; }
    }
}
