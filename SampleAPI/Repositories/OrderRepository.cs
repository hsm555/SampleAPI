using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SampleAPI.Entities;
using SampleAPI.Extensions;
using SampleAPI.Requests;

namespace SampleAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SampleApiDbContext _sampleApiDbContext;
        public OrderRepository(SampleApiDbContext sampleApiDbContext)
        {
             _sampleApiDbContext = sampleApiDbContext;
        }
        
        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns>List<Order></returns>
        public async Task<List<Order>> GetOrders()
        {
            var orders = await _sampleApiDbContext.Orders.Where(x => x.EntryDate >= DateTime.Now.AddDays(-1) && !x.IsDeleted).ToListAsync();
            return orders;
        }

        /// <summary>
        /// Create an order
        /// </summary>
        /// <param name="createOrderRequest">createOrderRequest</param>
        /// <returns>Order</returns>
        public async Task<Order> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            var order = new Order()
            { 
                Name = createOrderRequest.Name ?? string.Empty,
                Description = createOrderRequest.Description ?? string.Empty
            };

            var result = await _sampleApiDbContext.AddAsync(order);
            await _sampleApiDbContext.SaveChangesAsync();
            return result.Entity;
        }

        /// <summary>
        /// Get all orders within current day - number of days excluding weekend
        /// </summary>
        /// <param name="numberOfDays">numberOfDays</param>
        /// <returns>List<Order></returns>
        public async Task<List<Order>> GetOrdersByDays(int numberOfDays)
        {
            var orders = await _sampleApiDbContext.Orders.Where(x => x.EntryDate >= DateTime.Now.AddBusinessDays(numberOfDays * -1) && !x.IsDeleted).ToListAsync();
            return orders;
        }
    }
}
