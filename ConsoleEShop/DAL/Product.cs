namespace DAL
{
    public class Product
    {
        private static int _number;
        public int Id { get;}
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Product(string name, string category, string description, decimal price)
        {
            Id = ++_number;
            Name = name;
            Category = category;
            Description = description;
            Price = price;
        }

        public Product()
        {
            Name = "";
            Category = "";
            Description = "";
            Price = 0;
        }
        public void InfoFromProduct(Product newProduct)
        {
            Name = newProduct.Name;
            Category = newProduct.Category;
            Description = newProduct.Description;
            Price = newProduct.Price;
        }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Category: {Category}, Description: {Description}, Price: {Price}";
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Product product)) return false;
            return Name == product.Name && Category == product.Category && Description == product.Description &&
                   Price == product.Price;
        }
        protected bool Equals(Product other)
        {
            return Id == other.Id && string.Equals(Name, other.Name) && string.Equals(Category, other.Category) && string.Equals(Description, other.Description) && Price == other.Price;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Category != null ? Category.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) Price;
                return hashCode;
            }
        }
    }
}
