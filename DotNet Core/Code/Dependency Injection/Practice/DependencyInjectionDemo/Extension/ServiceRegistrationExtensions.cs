using DependencyInjectionDemo.Services;

namespace DependencyInjectionDemo.Extension
{
    /// <summary>
    /// Contains extension methods for registering services in the IoC container.
    /// This helps organize service registrations and keeps the Program.cs file clean.
    /// </summary>
    public static class ServiceRegistrationExtensions
    {
        /// <summary>
        /// Adds product-related services to the IoC container.
        /// This method registers the ProductService for dependency injection.
        /// </summary>
        /// <param name="services">The IServiceCollection used to register services in the IoC container.</param>
        public static void AddProductServices(this IServiceCollection services)
        {
            // Register IProductService with its implementation ProductService as Transient
            services.AddTransient<IProductService, ProductService>();
        }
    }
}
