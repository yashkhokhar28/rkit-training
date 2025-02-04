namespace DependencyInjectionDemo.Services
{
    /// <summary>
    /// Implementation of the IPaymentService interface for processing payments via PayPal.
    /// This service handles the specific logic required to process a payment through PayPal.
    /// </summary>
    public class PayPalPaymentService : IPaymentService
    {
        /// <summary>
        /// Processes a payment for the given order ID using PayPal.
        /// This method contains the logic for integrating with PayPal's payment system.
        /// </summary>
        /// <param name="orderId">The unique identifier for the order being paid for.</param>
        /// <returns>A confirmation message indicating that the payment has been processed via PayPal.</returns>
        public string ProcessPayment(int orderId)
        {
            // Logic for PayPal payment processing
            return $"Payment processed via PayPal for Order ID: {orderId}.";
        }
    }
}
