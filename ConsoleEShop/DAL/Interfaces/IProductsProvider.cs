using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IProductsProvider
    {
        List<(Product, int)> GetProducts();
        (Product, int) GetProduct(string name);
        bool AddProduct(Product product);
        bool ChangeProductInfo(int id, Product newProduct, int quantity);
        bool ContainsProduct(string name);
        bool ContainsProduct(int id);
        bool CheckInStock(List<(Product, int)> productsList);
        void ProductsReservation(List<(Product, int)> productsList);
    }
}
