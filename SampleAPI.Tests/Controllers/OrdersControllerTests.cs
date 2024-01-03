using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using SampleAPI.Controllers;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using SampleAPI.Tests.MockData;

namespace SampleAPI.Tests.Controllers
{
    /// <summary>
    /// OrdersControllerTests class
    /// </summary>
    public class OrdersControllerTests
    {
        /// <summary>
        /// GetOrders api method test
        /// </summary>
        /// <returns>200 status</returns>
        [Fact]
        public async Task GetOrders_ShouldReturn200Status()
        {
            var orderRepository = new Mock<IOrderRepository>();
            var logger = new Mock<ILogger<OrdersController>>();
            orderRepository.Setup(_ => _.GetOrders()).ReturnsAsync(OrderMockData.GetOrders());
            var sut = new OrdersController(orderRepository.Object, logger.Object);

            var response = await sut.GetOrders() as ObjectResult;

            response?.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// GetOrders api method test
        /// </summary>
        /// <returns>204 status</returns>
        [Fact]
        public async Task GetOrders_ShouldReturn204Status()
        {
            var orderRepository = new Mock<IOrderRepository>();
            var logger = new Mock<ILogger<OrdersController>>();
            orderRepository.Setup(_ => _.GetOrders()).ReturnsAsync(OrderMockData.EmptyOrders());
            var sut = new OrdersController(orderRepository.Object, logger.Object);

            var response = await sut.GetOrders() as ObjectResult;

            response?.StatusCode.Should().Be(204);
        }

        /// <summary>
        /// CreateOrder api method test
        /// </summary>
        /// <returns>201 status</returns>
        [Fact]
        public async Task CreateOrder_ShouldReturn201Status()
        {
            var orderRepository = new Mock<IOrderRepository>();
            var logger = new Mock<ILogger<OrdersController>>();
            var request = new CreateOrderRequest() { Name = "Test" , Description = "Test"};
            orderRepository.Setup(_ => _.CreateOrder(request)).ReturnsAsync(OrderMockData.GetOrder());
            var sut = new OrdersController(orderRepository.Object, logger.Object);

            var response = await sut.CreateOrder(request) as ObjectResult;

            response?.StatusCode.Should().Be(201);
        }

        /// <summary>
        /// GetOrdersByDays api method test
        /// </summary>
        /// <returns>200 status</returns>
        [Fact]
        public async Task GetOrdersByDays_ShouldReturn200Status()
        {
            int numberOfDays = 4;
            var orderRepository = new Mock<IOrderRepository>();
            var logger = new Mock<ILogger<OrdersController>>();
            orderRepository.Setup(_ => _.GetOrdersByDays(numberOfDays)).ReturnsAsync(OrderMockData.GetOrders());
            var sut = new OrdersController(orderRepository.Object, logger.Object);

            var response = await sut.GetOrdersByDays(numberOfDays) as ObjectResult;

            response?.StatusCode.Should().Be(200);
        }
    }
}
