namespace WooliesX.Challenge.Api.Services
{
    public class Product
    {
        public string Name { get; }

        public decimal Price { get; }

        public decimal Quantity { get; }

        public Product(string name, decimal price, decimal quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}