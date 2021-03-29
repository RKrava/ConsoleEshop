using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DAL.DataBases;
using DAL.Enums;
using DAL.Interfaces;

namespace BLL.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrdersProvider _ordersDb;

        public OrderService()
        {
            _ordersDb = new OrdersDb();
        }
        public bool AddOrder(List<(Product, int)> productList, IUser customer, out int id)
        {
            id = _ordersDb.AddOrder(new Order(productList, customer));
            return true;
        }

        public bool ContainOrder(int id)
        {
            return _ordersDb.ContainOrder(id);
        }

        public Order GetOrder(int id)
        {
            return _ordersDb.GetOrder(id);
        }

        public Order GetOrder(int id, IUser user)
        {
            return _ordersDb.GetOrder(id, user);
        }

        public List<Order> GetOrders()
        {
            return _ordersDb.GetOrders();
        }

        public void SetStatus(Order order, Status status)
        {
            order.Status = status;
        }
    }
}
