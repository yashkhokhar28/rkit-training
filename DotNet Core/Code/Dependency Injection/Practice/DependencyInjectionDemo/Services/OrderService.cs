namespace DependencyInjectionDemo.Services
{
    /// <summary>
    /// Service responsible for handling order-related operations.
    /// This service includes methods for creating and managing orders.
    /// </summary>
    public class OrderService
    {
        /// <summary>
        /// Creates a new order for a given customer.
        /// </summary>
        /// <param name="orderId">The unique identifier for the order.</param>
        /// <param name="customerName">The name of the customer placing the order.</param>
        /// <returns>A confirmation message indicating that the order has been created.</returns>
        public string CreateOrder(int orderId, string customerName)
        {
            // Returns a message confirming the creation of the order with the given ID and customer name.
            return $"Order with ID: {orderId} created for customer: {customerName}.";
        }
    }
}
