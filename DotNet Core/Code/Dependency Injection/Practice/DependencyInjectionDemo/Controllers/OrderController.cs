using DependencyInjectionDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionDemo.Controllers
{
    /// <summary>
    /// Controller to manage order-related operations.
    /// Provides endpoints to create orders and process payments.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// Service to handle order operations.
        /// Responsible for creating and managing orders.
        /// </summary>
        private readonly OrderService _orderService;

        /// <summary>
        /// Service to handle payment operations.
        /// Responsible for processing payments for orders.
        /// </summary>
        private readonly IPaymentService _paymentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="orderService">The order service used to handle order-related logic.</param>
        /// <param name="paymentService">The payment service used to handle payment processing.</param>
        public OrderController(OrderService orderService, IPaymentService paymentService)
        {
            _orderService = orderService;  // Injecting OrderService to handle order creation
            _paymentService = paymentService;  // Injecting PaymentService to handle payment processing
        }

        /// <summary>
        /// Creates a new order and processes the payment.
        /// This endpoint handles the full order creation process, including payment.
        /// </summary>
        /// <param name="orderId">The ID of the order to create.</param>
        /// <param name="customerName">The name of the customer placing the order.</param>
        /// <returns>Returns an object containing both order confirmation and payment confirmation.</returns>
        [HttpPost("createOrder")]
        public IActionResult CreateOrder(int orderId, string customerName)
        {
            // Call the order service to create an order
            var orderConfirmation = _orderService.CreateOrder(orderId, customerName);

            // Call the payment service to process the payment for the order
            var paymentConfirmation = _paymentService.ProcessPayment(orderId);

            // Return both order and payment confirmation as part of the response
            return Ok(new { orderConfirmation, paymentConfirmation });
        }
    }
}
