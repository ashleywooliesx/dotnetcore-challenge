using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using NSubstitute;

using WooliesX.Challenge.Api.Controllers;
using WooliesX.Challenge.Api.Queries;
using WooliesX.Challenge.Api.Services;

using Xunit;

namespace WooliesX.Challenge.UnitTests
{
    public class GetSortedProductsQueryHandlerTests
    {
        private readonly GetSortedProductsQuery.GetSortedProductsQueryHandler _handler;

        private readonly Product _productA;

        private readonly Product _productB;

        private readonly Product _productC;

        private readonly Product _productD;

        private readonly Product _productF;

        public GetSortedProductsQueryHandlerTests()
        {
            _productA = new Product("Product-A", 400, 1);
            _productB = new Product("Product-B", 300, 2);
            _productC = new Product("Product-C", 200, 3);
            _productD = new Product("Product-D", 100, 4);
            _productF = new Product("Product-F", 500, 5);

            var products = new[]
            {
                _productA,
                _productB,
                _productC,
                _productD,
                _productF,
            };

            var mockProductsService = Substitute.For<IProductsService>();
            mockProductsService
                .GetProducts(Arg.Any<CancellationToken>())
                .Returns(products);

            var shoppingHistory = new[]
            {
                new CustomerProducts(
                    123,
                    new[]
                    {
                        new Product("Product-A", 100, 3),
                        new Product("Product-B", 100, 1),
                        new Product("Product-F", 100, 1),
                    }),
                new CustomerProducts(
                    23,
                    new[]
                    {
                        new Product("Product-A", 100, 2),
                        new Product("Product-B", 100, 3),
                        new Product("Product-F", 100, 1),
                    }),
                new CustomerProducts(
                    23,
                    new[]
                    {
                        new Product("Product-C", 100, 2),
                        new Product("Product-F", 100, 2),
                    }),
                new CustomerProducts(
                    23,
                    new[]
                    {
                        new Product("Product-A", 100, 1),
                        new Product("Product-B", 100, 1),
                        new Product("Product-C", 100, 1),
                    })
            };

            mockProductsService
                .GetShoppingHistory(Arg.Any<CancellationToken>())
                .Returns(shoppingHistory);

            _handler = new GetSortedProductsQuery.GetSortedProductsQueryHandler(mockProductsService);
        }

        [Fact]
        public async Task GivenProductsExistWhenSortedByLowThenResponseIsSortedCorrectly()
        {
            // Arrange
            var request = new GetSortedProductsQuery(SortOption.Low);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response
                .Products
                .Should()
                .ContainInOrder(_productD, _productC, _productB, _productA, _productF);
        }

        [Fact]
        public async Task GivenProductsExistWhenSortedByHighThenResponseIsSortedCorrectly()
        {
            // Arrange
            var request = new GetSortedProductsQuery(SortOption.High);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response
                .Products
                .Should()
                .ContainInOrder(_productF, _productA, _productB, _productC, _productD);
        }

        [Fact]
        public async Task GivenProductsExistWhenSortedByAscendingThenResponseIsSortedCorrectly()
        {
            // Arrange
            var request = new GetSortedProductsQuery(SortOption.Ascending);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response
                .Products
                .Should()
                .ContainInOrder(_productA, _productB, _productC, _productD, _productF);
        }

        [Fact]
        public async Task GivenProductsExistWhenSortedByDescendingThenResponseIsSortedCorrectly()
        {
            // Arrange
            var request = new GetSortedProductsQuery(SortOption.Descending);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response
                .Products
                .Should()
                .ContainInOrder(_productF, _productD, _productC, _productB, _productA);
        }

        [Fact]
        public async Task GivenProductsExistWhenSortedByRecommendedThenResponseIsSortedCorrectly()
        {
            // Arrange
            var request = new GetSortedProductsQuery(SortOption.Recommended);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response
                .Products
                .Select(p => p.Name)
                .Should()
                .ContainInOrder("Product-A", "Product-B", "Product-F", "Product-C");
        }
    }
}
