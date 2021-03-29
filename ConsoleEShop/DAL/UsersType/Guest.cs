using DAL.Enums;
using DAL.Interfaces;

namespace DAL.UsersType
{
    public class Guest : IUser
    {
        public Guest()
        {
            Rights = Rights.Guest;
        }

        public Rights Rights { get; set; }
    }
}
