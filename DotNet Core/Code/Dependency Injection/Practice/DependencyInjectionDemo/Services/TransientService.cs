namespace DependencyInjectionDemo.Services
{
    /// <summary>
    /// Implementation of the ITransientService interface.
    /// This service is created every time it is requested.
    /// A unique operation ID is generated for each instance of the service.
    /// </summary>
    public class TransientService : ITransientService
    {
        /// <summary>
        /// The unique operation ID for this transient service instance.
        /// A new ID is generated every time a new instance of the service is created.
        /// </summary>
        private readonly Guid _operationId;

        /// <summary>
        /// Initializes a new instance of the TransientService class.
        /// A unique operation ID is generated every time a new instance is created.
        /// </summary>
        public TransientService()
        {
            _operationId = Guid.NewGuid();  // Generate a unique GUID for the operation ID
        }

        /// <summary>
        /// Retrieves the unique operation ID for the current instance of the service.
        /// This ID is different for each instance since the service is created anew each time.
        /// </summary>
        /// <returns>A GUID representing the unique operation ID for the current instance of the service.</returns>
        public Guid GetOperationId() => _operationId;
    }
}
