namespace DependencyInjectionDemo.Services
{
    /// <summary>
    /// Implementation of the ISingletonService interface.
    /// This service is created once and shared throughout the application's lifetime.
    /// A unique operation ID is generated when the service is first created and shared across all requests.
    /// </summary>
    public class SingletonService : ISingletonService
    {
        /// <summary>
        /// The unique operation ID for this singleton service instance.
        /// This ID is generated once and remains the same throughout the application's lifetime.
        /// </summary>
        private readonly Guid _operationId;

        /// <summary>
        /// Initializes a new instance of the SingletonService class.
        /// A unique operation ID is generated only once when the service is first created.
        /// </summary>
        public SingletonService()
        {
            _operationId = Guid.NewGuid();  // Generate a unique GUID for the operation ID
        }

        /// <summary>
        /// Retrieves the unique operation ID for the current singleton service instance.
        /// This ID is consistent across all requests since the service is shared for the application's lifetime.
        /// </summary>
        /// <returns>A GUID representing the unique operation ID for the singleton service instance.</returns>
        public Guid GetOperationId() => _operationId;
    }
}
