namespace DependencyInjectionDemo.Services
{
    /// <summary>
    /// Implementation of the IPaymentService interface for processing payments via Stripe.
    /// This service handles the specific logic required to process a payment through Stripe.
    /// </summary>
    public class StripePaymentService : IPaymentService
    {
        /// <summary>
        /// Processes a payment for the given order ID using Stripe.
        /// This method contains the logic for integrating with Stripe's payment system.
        /// </summary>
        /// <param name="orderId">The unique identifier for the order being paid for.</param>
        /// <returns>A confirmation message indicating that the payment has been processed via Stripe.</returns>
        public string ProcessPayment(int orderId)
        {
            // Logic for Stripe payment processing
            return $"Payment processed via Stripe for Order ID: {orderId}.";
        }
    }
}
