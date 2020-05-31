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

        public GetSortedProductsQueryHandlerTests()
        {
            _productA = new Product("Product-A", 400, 1);
            _productB = new Product("Product-B", 300, 2);
            _productC = new Product("Product-C", 200, 3);
            _productD = new Product("Product-D", 100, 4);
            
            var products = new[]
            {
                _productA,
                _productB,
                _productC,
                _productD,
            };

            var mockProductsService = Substitute.For<IProductsService>();
            mockProductsService
                .GetProducts(Arg.Any<CancellationToken>())
                .Returns(products);

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
                .ContainInOrder(_productD, _productC, _productB, _productA);
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
                .ContainInOrder(_productA, _productB, _productC, _productD);
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
                .ContainInOrder(_productA, _productB, _productC, _productD);
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
                .ContainInOrder(_productD, _productC, _productB, _productA);
        }
    }
}
