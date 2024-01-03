using Microsoft.AspNetCore.Mvc;
using SampleAPI.Entities;
using SampleAPI.Requests;

namespace SampleAPI.Repositories
{
    /// <summary>
    /// IOrderRepository interface
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns>List<Order></returns>
        Task<List<Order>> GetOrders();

        /// <summary>
        /// Create an order
        /// </summary>
        /// <param name="createOrderRequest">createOrderRequest</param>
        /// <returns>Order</returns>
        Task<Order> CreateOrder(CreateOrderRequest createOrderRequest);

        /// <summary>
        /// Get all orders within current day - number of days excluding weekend
        /// </summary>
        /// <param name="numberOfDays">numberOfDays</param>
        /// <returns>List<Order></returns>
        Task<List<Order>> GetOrdersByDays(int numberOfDays);
    }
}
