using Example.Api.Models;
using Example.Api.Clients;
using Example.Domain.Models;
using Example.Domain.Workflows;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly OrderWorkflow _orderWorkflow;
        private readonly ShoppingApiClient _shoppingApiClient;

        public OrdersController(ILogger<OrdersController> logger, OrderWorkflow orderWorkflow, ShoppingApiClient shoppingApiClient)
        {
            _logger = logger;
            _orderWorkflow = orderWorkflow;
            _shoppingApiClient = shoppingApiClient;
        }

        // Endpoint pentru plasarea unei comenzi
        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderModel orderModel)
        {
            var result = await _shoppingApiClient.PlaceOrderAsync(orderModel);
            if (result == null)
                return BadRequest("Comanda nu a putut fi procesată.");

            return Ok(result);
        }

        // Endpoint pentru procesarea unei comenzi după validări
        [HttpPost("validate-order")]
        public async Task<IActionResult> ValidateOrder([FromBody] OrderModel orderModel)
        {
            var validationResult = await _orderWorkflow.ValidateOrderAsync(orderModel);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return Ok("Comanda a fost validată cu succes.");
        }
    }
}