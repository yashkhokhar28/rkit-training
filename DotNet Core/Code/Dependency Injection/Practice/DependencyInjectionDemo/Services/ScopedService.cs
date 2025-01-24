namespace DependencyInjectionDemo.Services
{
    /// <summary>
    /// Implementation of the IScopedService interface.
    /// This service is created once per HTTP request and provides a unique operation ID for each request.
    /// </summary>
    public class ScopedService : IScopedService
    {
        /// <summary>
        /// The unique operation ID for this service instance.
        /// This ID is generated when the service is created and remains the same throughout the request.
        /// </summary>
        private readonly Guid _operationId;

        /// <summary>
        /// Initializes a new instance of the ScopedService class.
        /// A unique operation ID is generated when the service is created.
        /// </summary>
        public ScopedService()
        {
            _operationId = Guid.NewGuid();  // Generate a unique GUID for the operation ID
        }

        /// <summary>
        /// Retrieves the unique operation ID for the current instance of the service.
        /// This ID is consistent for the entire scope (HTTP request).
        /// </summary>
        /// <returns>A GUID representing the unique operation ID for this service instance.</returns>
        public Guid GetOperationId() => _operationId;
    }
}
