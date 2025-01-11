using Example.Api.Models;
using Example.Api.Clients;
using Example.Domain.Models;
using Example.Domain.WorkFlows;
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
        private readonly PlasareComandaWorkflow _plasareComandaWorkflow;
        private readonly FacturareWorkFlow _facturareWorkFlow;
        private readonly LivrareWorkFlow _livrareWorkFlow;
        private readonly ShoppingApiClient _shoppingApiClient;

        public OrdersController(
            ILogger<OrdersController> logger, 
            PlasareComandaWorkflow plasareComandaWorkflow,
            FacturareWorkFlow facturareWorkFlow,
            LivrareWorkFlow livrareWorkFlow,
            ShoppingApiClient shoppingApiClient)
        {
            _logger = logger;
            _plasareComandaWorkflow = plasareComandaWorkflow;
            _facturareWorkFlow = facturareWorkFlow;
            _livrareWorkFlow = livrareWorkFlow;
            _shoppingApiClient = shoppingApiClient;
        }

        // Endpoint pentru plasarea unei comenzi
        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderModel orderModel)
        {
            var result = await _shoppingApiClient.PlaceOrderAsync(orderModel);
            if (result == null)
                return BadRequest("Comanda nu a putut fi procesată.");

            // Dacă plasarea comenzii este validă, se trece la facturare și livrare
            var orderValidationResult = await _plasareComandaWorkflow.ValidateOrderAsync(orderModel);
            if (!orderValidationResult.IsValid)
                return BadRequest(orderValidationResult.Errors);

            await _facturareWorkFlow.ProcessOrderAsync(orderModel);
            await _livrareWorkFlow.ProcessOrderAsync(orderModel);

            return Ok("Comanda a fost plasată și procesată cu succes.");
        }

        // Endpoint pentru procesarea unei comenzi după validări
        [HttpPost("validate-order")]
        public async Task<IActionResult> ValidateOrder([FromBody] OrderModel orderModel)
        {
            var validationResult = await _plasareComandaWorkflow.ValidateOrderAsync(orderModel);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return Ok("Comanda a fost validată cu succes.");
        }
    }
}
