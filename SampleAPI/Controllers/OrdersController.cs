using Microsoft.AspNetCore.Mvc;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using Serilog;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderRepository orderRepository, ILogger<OrdersController> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
 
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status204NoContent, Type = typeof(NoContentResult))]
        public async Task<ActionResult> GetOrders()
        {
            try
            {
                var response = await _orderRepository.GetOrders();
                if (response.Count == 0)
                {
                    return NoContent();
                }

                return new ObjectResult(response) { StatusCode =  StatusCodes.Status200OK };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ObjectResult(ex) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        /// TODO: Add an endpoint to allow users to create an order using <see cref="CreateOrderRequest"/>.
        /// 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            try
            {
                var response = await _orderRepository.CreateOrder(createOrderRequest);
                if (response != null)
                {
                    return new ObjectResult(response) { StatusCode =  StatusCodes.Status201Created};
                }

                return new ObjectResult(response) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ObjectResult(ex) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        /// TODO: Add an endpoint to allow users to get orders by number of days using <see cref="GetOrdersByDays"/>.
        /// 
        [HttpGet("{numberOfDays}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType (StatusCodes.Status204NoContent, Type = typeof(NoContentResult))]
        public async Task<ActionResult> GetOrdersByDays(int numberOfDays)
        {
            try
            {
                if (numberOfDays < 0)
                {
                    return ValidationProblem("invalid number");
                }

                var response = await _orderRepository.GetOrdersByDays(numberOfDays);
                if (response.Count == 0)
                {
                    return NoContent();
                }

                return new ObjectResult(response) { StatusCode =  StatusCodes.Status200OK };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ObjectResult(ex) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}
