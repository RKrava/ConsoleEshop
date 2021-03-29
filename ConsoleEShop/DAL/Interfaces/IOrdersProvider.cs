using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IOrdersProvider
    {
        int AddOrder(Order order);
        bool ContainOrder(int id);
        Order GetOrder(int id);
        Order GetOrder(int id, IUser user);
        List<Order> GetOrders();
    }
}
