using System.Collections.Generic;
using DAL.Interfaces;

namespace DAL.DataBases
{
    public class OrdersDb : IOrdersProvider
    {
        private readonly List<Order> _orders;

        public OrdersDb()
        {
            _orders = new List<Order>();
        }

        public int AddOrder(Order order)
        {
            _orders.Add(order);
            return order.Id;
        }

        public bool ContainOrder(int id)
        {
            return _orders.Exists(x => x.Id == id);
        }
        public Order GetOrder(int id)
        {
            return _orders.Find(x => x.Id == id);
        }
        public Order GetOrder(int id, IUser user)
        {
            var order = _orders.Find(x => x.Id == id);
            return user != order.Customer ? null : order;
        }
        public List<Order> GetOrders()
        {
            return _orders;
        }
    }
}
