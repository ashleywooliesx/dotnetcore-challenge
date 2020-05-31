using System.Collections.Generic;

using Newtonsoft.Json;

namespace WooliesX.Challenge.Api.Requests
{
    public class Trolley
    {
        public IEnumerable<ProductPrice> Products { get; set; }

        public IEnumerable<Special> Specials { get; set; }

        public IEnumerable<ProductQuantity> Quantities { get; set; }
    }

    public class ProductQuantity
    {
        public ProductQuantity()
        {
            
        }

        public ProductQuantity(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public string Name { get; set; }

        public int Quantity { get; set; }
    }

    public class Special
    {
        public Special()
        {
            
        }

        public Special(IEnumerable<ProductQuantity> quantities, decimal total)
        {
            Quantities = quantities;
            Total = total;
        }

        public IEnumerable<ProductQuantity> Quantities { get; set; }

        public decimal Total { get; set; }
    }

    public class ProductPrice
    {
        public ProductPrice()
        {
            
        }

        public ProductPrice(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}