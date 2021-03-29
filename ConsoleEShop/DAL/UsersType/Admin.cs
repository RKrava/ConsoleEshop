using DAL.Enums;
using DAL.Interfaces;

namespace DAL.UsersType
{
    public class Admin : IUser
    {
        public Admin()
        {
            Rights = Rights.Admin;
        }

        public Rights Rights { get; set; }
    }
}
