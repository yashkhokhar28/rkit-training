namespace DependencyInjectionDemo.Services
{
    /// <summary>
    /// Interface for services that are created per HTTP request (Scoped).
    /// This service should return a unique operation ID for each request.
    /// </summary>
    public interface IScopedService
    {
        /// <summary>
        /// Retrieves a unique operation ID for the current request.
        /// This ID is used to distinguish between different instances of the service for each HTTP request.
        /// </summary>
        /// <returns>A GUID representing the operation ID for the current request.</returns>
        Guid GetOperationId();
    }
}
