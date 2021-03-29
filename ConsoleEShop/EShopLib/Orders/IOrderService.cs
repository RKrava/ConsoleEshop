using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DAL.Enums;
using DAL.Interfaces;

namespace BLL.Orders
{
    public interface IOrderService
    {
        bool AddOrder(List<(Product, int)> productList, IUser customer, out int id);
        bool ContainOrder(int id);
        Order GetOrder(int id);
        Order GetOrder(int id, IUser user);
        List<Order> GetOrders();
        void SetStatus(Order order, Status status);
    }
}
