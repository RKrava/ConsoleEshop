using System.Collections.Generic;
using DAL;
using DAL.DataBases;
using DAL.Interfaces;

namespace BLL.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductsProvider _productsDb;

        public ProductService()
        {
            _productsDb = new ProductsDB();
        }
        public List<(Product, int)> GetProducts()
        {
            return _productsDb.GetProducts();
        }

        public (Product, int) GetProduct(string name)
        {
            return _productsDb.GetProduct(name);
        }

        public bool AddProduct(string name, string category, string description, decimal price)
        {
            return _productsDb.AddProduct(new Product(name,category,description,price));
        }

        public bool ChangeProductInfo(int id, Product newProduct, int newQuantity)
        {
            return _productsDb.ChangeProductInfo(id, newProduct, newQuantity);
        }

        public bool ContainsProduct(string name)
        {
            return _productsDb.ContainsProduct(name);
        }

        public bool ContainsProduct(int id)
        {
            return _productsDb.ContainsProduct(id);
        }

        public bool CheckInStock(List<(Product, int)> productsList)
        {
            return _productsDb.CheckInStock(productsList);
        }

        public void ProductsReservation(List<(Product, int)> productsList)
        {
            _productsDb.ProductsReservation(productsList);
        }
    }
}
