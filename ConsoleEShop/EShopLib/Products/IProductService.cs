using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace BLL.Products
{
    public interface IProductService
    {
        List<(Product,int)> GetProducts();
        (Product, int) GetProduct(string name);
        bool AddProduct(string name, string category, string description, decimal price);
        bool ChangeProductInfo(int id, Product newProduct, int newQuantity);
        bool ContainsProduct(string name);
        bool ContainsProduct(int id);
        bool CheckInStock(List<(Product, int)> productsList);
        void ProductsReservation(List<(Product, int)> productsList);

    }
}
