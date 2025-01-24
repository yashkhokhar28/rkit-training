namespace DependencyInjectionDemo.Services
{
    /// <summary>
    /// Interface for handling payment operations.
    /// This service is responsible for processing payments for orders.
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Processes a payment for the given order.
        /// This method is responsible for handling the logic of processing a payment, such as charging the customer.
        /// </summary>
        /// <param name="orderId">The ID of the order for which the payment is being processed.</param>
        /// <returns>A confirmation message indicating whether the payment was successful.</returns>
        string ProcessPayment(int orderId);
    }
}
