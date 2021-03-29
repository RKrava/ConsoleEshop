using System.Collections.Generic;
using DAL.Interfaces;

namespace DAL.DataBases
{
    public class ProductsDB : IProductsProvider
    {
        private readonly List<(Product, int)> _products;

        public ProductsDB()
        {
            _products = new List<(Product, int)>() {(new Product("Ball", "Toys","Regular ball",5),7), (new Product("Doll", "Toys", "Rubber doll", 9), 15), (new Product("IPhone", "Electronics", "Very expensive phone", 1500), 1), (new Product("TV", "Electronics", "Big TV", 750), 25) };
        }

        public List<(Product, int)> GetProducts()
        {
            return _products;
        }

        public (Product, int) GetProduct(string name)
        {
            return _products.Find(x => x.Item1.Name == name);
        }

        public bool AddProduct(Product product)
        {
            if (_products.Exists(x => x.Item1.Equals(product))) return false;
            _products.Add((product, 0));
            return true;
        }

        public bool ChangeProductInfo(int id, Product newProduct, int quantity)
        {
            if (!_products.Exists(x => x.Item1.Id == id)) return false;
            var existProduct = _products.Find(x => x.Item1.Id == id);
            _products.Remove(existProduct);
            existProduct.Item1.InfoFromProduct(newProduct);
            existProduct.Item2 = quantity;
            _products.Add(existProduct);
            return true;
        }

        public bool ContainsProduct(string name)
        {
            return _products.Exists(x => x.Item1.Name == name);
        }
        public bool ContainsProduct(int id)
        {
            return _products.Exists(x => x.Item1.Id == id);
        }

        public bool CheckInStock(List<(Product, int)> productsList)
        {
            foreach (var (product, quantity) in productsList)
            {
                if (_products.Find(x => Equals(x.Item1, product)).Item2 < quantity)
                {
                    return false;
                }
            }
            return true;
        }

        public void ProductsReservation(List<(Product, int)> productsList)
        {
            foreach (var (product, quantity) in productsList)
            {
                var existProduct = _products.Find(x => x.Item1.Id == product.Id);
                _products.Remove(existProduct);
                existProduct.Item2 -= quantity;
                _products.Add(existProduct);
            }
        }
    }
}
