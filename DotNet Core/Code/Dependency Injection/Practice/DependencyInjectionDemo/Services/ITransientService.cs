namespace DependencyInjectionDemo.Services
{
    /// <summary>
    /// Interface for services that are created every time they are requested (Transient).
    /// This service should return a unique operation ID for each instance.
    /// </summary>
    public interface ITransientService
    {
        /// <summary>
        /// Retrieves a unique operation ID for each instance of the service.
        /// A new operation ID is generated every time the service is requested.
        /// </summary>
        /// <returns>A GUID representing the operation ID for the current instance of the service.</returns>
        Guid GetOperationId();
    }
}
