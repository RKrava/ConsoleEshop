using System.Collections.Generic;
using DAL.Enums;
using DAL.Interfaces;

namespace DAL
{
    public class Order
    {
        private static int _numberOfOrders = 0;
        public int Id { get; private set; }
        public List<(Product,int)> ProductsList { get; set; }
        public IUser Customer;
        public Status Status { get; set; }

        public Order(List<(Product, int)> productsList, IUser customer)
        {
            Id = ++_numberOfOrders;
            Customer = customer;
            ProductsList = productsList;
            Status = Status.New;
        }

        public decimal GetPrice()
        {
            decimal sumPrice = 0;
            foreach (var (product, quantity) in ProductsList)
            {
                sumPrice += product.Price * quantity;
            }

            return sumPrice;
        }
        public override string ToString()
        {
            var orderToString = $"Order id: {Id}\nProducts List:\n";
            foreach (var (product, quantity) in ProductsList)
            {
                orderToString += $"{product} - Quantity: {quantity}\n";
            }

            orderToString += "Status: ";
            switch (Status)
            {
                case Status.New:
                    orderToString += "New";
                    break;
                case Status.CanceledByAdmin:
                    orderToString += "Canceled by admin";
                    break;
                case Status.CanceledByUser:
                    orderToString += "Canceled by user";
                    break;
                case Status.Completed:
                    orderToString += "Completed";
                    break;
                case Status.PaymentReceived:
                    orderToString += "Payment received";
                    break;
                case Status.Sent:
                    orderToString += "Sent";
                    break;
                case Status.Received:
                    orderToString += "Received";
                    break;
                case Status.InProgress:
                    orderToString += "In progress";
                    break;
            }

            orderToString += "\n";
            return orderToString;
        }
    }
}
