using DAL.Enums;

namespace DAL.Interfaces
{
    public interface IUser
    {
        Rights Rights { get; set; }
    }
}
