using System.Collections.Generic;

namespace WooliesX.Challenge.Api.Services
{
    public class CustomerProducts
    {
        public int CustomerId { get; }

        public IEnumerable<Product> Products { get; }

        public CustomerProducts(int customerId, IEnumerable<Product> products)
        {
            CustomerId = customerId;
            Products = products;
        }
    }
}