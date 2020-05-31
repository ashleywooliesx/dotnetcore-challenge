using WooliesX.Challenge.Api.Services;

namespace WooliesX.Challenge.Api.Queries
{
    internal class SoldProduct
    {
        public Product Product { get; }

        public SoldProduct(Product product)
        {
            Product = product;
            QuantitySold = product.Quantity;
            SoldCount = 1;
        }

        public decimal QuantitySold { get; set; }

        public int SoldCount { get; set; }
    }
}