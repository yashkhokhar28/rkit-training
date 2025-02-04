namespace DependencyInjectionDemo.Services
{
    /// <summary>
    /// Interface for services that are created once and shared throughout the application's lifetime (Singleton).
    /// This service should return a consistent operation ID across all requests.
    /// </summary>
    public interface ISingletonService
    {
        /// <summary>
        /// Retrieves a unique operation ID for the service.
        /// The same operation ID is shared across all instances of the Singleton service.
        /// </summary>
        /// <returns>A GUID representing the operation ID for the service.</returns>
        Guid GetOperationId();
    }
}
