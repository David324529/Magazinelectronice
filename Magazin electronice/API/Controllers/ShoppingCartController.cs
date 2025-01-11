using Example.Api.Models;
using Example.Api.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Example.Domain.Models;
using Example.Domain.Workflows;
using System.Linq;
using Examples.Domain.Workflows;

namespace Example.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly ShoppingApiClient _shoppingApiClient;
        private readonly PlasareComandaWorkflow _plasareComandaWorkflow;
        private readonly FacturareWorkflow _facturareWorkflow;
        private readonly LivrareWorkflow _livrareWorkflow;

        public ShoppingCartController(
            ILogger<ShoppingCartController> logger,
            ShoppingApiClient shoppingApiClient,
            PlasareComandaWorkflow plasareComandaWorkflow,
            FacturareWorkflow facturareWorkflow,
            LivrareWorkflow livrareWorkflow)
        {
            _logger = logger;
            _shoppingApiClient = shoppingApiClient;
            _plasareComandaWorkflow = plasareComandaWorkflow;
            _facturareWorkflow = facturareWorkflow;
            _livrareWorkflow = livrareWorkflow;
        }

        // Endpoint pentru vizualizarea coșului de cumpărături
        [HttpGet("{customerId}")]
        public IActionResult GetCart(int customerId)
        {
            var cart = _shoppingApiClient.GetCartAsync(customerId).Result;
            if (cart == null)
                return NotFound("Coșul de cumpărături nu a fost găsit.");

            return Ok(cart);
        }

        // Endpoint pentru adăugarea unui produs în coș
        [HttpPost("{customerId}/add")]
        public IActionResult AddToCart(int customerId, [FromBody] CartItemModel cartItem)
        {
            var result = _shoppingApiClient.AddProductToCartAsync(customerId, cartItem).Result;
            return Ok(result);
        }

        // Endpoint pentru finalizarea comenzii
        [HttpPost("{customerId}/checkout")]
        public IActionResult Checkout(int customerId)
        {
            var cart = _shoppingApiClient.GetCartAsync(customerId).Result;

            if (cart == null || cart.Items.Count == 0)
                return BadRequest("Coșul de cumpărături este gol.");

            var orderModel = new OrderModel
            {
                OrderItems = cart.Items.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList(),
                CustomerId = customerId
            };

            // Procesăm plasarea comenzii
            var result = _plasareComandaWorkflow.Execute(orderModel);

            if (result is OrderProcessFailedEvent failedEvent)
            {
                return BadRequest(failedEvent.Message); // Returnează eroarea din plasare
            }

            var order = (Order)result; // Comanda validă
            result = _facturareWorkflow.Execute(order);

            if (result is OrderProcessFailedEvent failedFacturare)
            {
                return BadRequest(failedFacturare.Message); // Returnează eroarea din facturare
            }

            result = _livrareWorkflow.Execute(order);

            if (result is OrderProcessFailedEvent failedLivrare)
            {
                return BadRequest(failedLivrare.Message); // Returnează eroarea din livrare
            }

            return Ok(result); // Returnează comanda livrată cu succes
        }
    }
}
