using FluentAssertions;
using FluentAssertions.Extensions;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using SampleAPI.Tests.MockData;

namespace SampleAPI.Tests.Repositories
{
    /// <summary>
    /// OrderRepositoryTests class
    /// </summary>
    public class OrderRepositoryTests : IDisposable
    {
        private readonly SampleApiDbContext _sampleApiDbContext;
        public OrderRepositoryTests()
        {
             _sampleApiDbContext = MockSampleApiDbContextFactory.GenerateMockContext();
        }

        /// <summary>
        /// GetOrders method test 
        /// </summary>
        /// <returns>All orders created within a day</returns>
        [Fact]
        public async Task GetOrders_ShouldReturnAllOrders()
        {
            _sampleApiDbContext.Orders.AddRange(OrderMockData.GetOrders());
            _sampleApiDbContext.SaveChanges();
            var sut = new OrderRepository(_sampleApiDbContext);

            var result = await sut.GetOrders();

            result.Should().HaveCount(OrderMockData.GetOrders().Count);
        }

        /// <summary>
        /// CreateOrder method test
        /// </summary>
        /// <returns>Return the order created</returns>
        [Fact]
        public async Task CreateOrder_ShouldCreateNewOrder()
        {
            _sampleApiDbContext.Orders.AddRange(OrderMockData.GetOrders());
            _sampleApiDbContext.SaveChanges();
            var sut = new OrderRepository(_sampleApiDbContext);
            var request = new CreateOrderRequest() { Name = "Test" , Description = "Test"};

            var result = await sut.CreateOrder(request);

            int expectedRecordCount = OrderMockData.GetOrders().Count + 1;
            _sampleApiDbContext.Orders.Count().Should().Be(expectedRecordCount);
        }

        /// <summary>
        /// GetOrdersByDays method test
        /// </summary>
        /// <returns>Returns orders within the days specified</returns>
        [Fact]
        public async Task GetOrdersByDays_ShouldReturnPastFourDaysOrders()
        {
            int numberOfDays = 4;
            _sampleApiDbContext.Orders.AddRange(OrderMockData.GetPastOrders());
            _sampleApiDbContext.SaveChanges();
            var sut = new OrderRepository(_sampleApiDbContext);

            var result = await sut.GetOrdersByDays(numberOfDays);

            result.Should().HaveCount(1);
        }

        /// <summary>
        /// dispose method
        /// </summary>
        public void Dispose()
        {
            _sampleApiDbContext.Database.EnsureDeleted();
            _sampleApiDbContext.Dispose();
        }
    }
}