using DependencyInjectionDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionDemo.Controllers
{
    /// <summary>
    /// Controller to demonstrate the different service lifetimes in dependency injection.
    /// It provides an endpoint to inspect the lifecycle of Singleton, Scoped, and Transient services.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceLifetimeController : ControllerBase
    {
        /// <summary>
        /// Singleton service 1.
        /// This service is shared throughout the application's lifetime.
        /// </summary>
        private readonly ISingletonService _singletonService1;

        /// <summary>
        /// Singleton service 2.
        /// Another instance of Singleton service for comparison.
        /// </summary>
        private readonly ISingletonService _singletonService2;

        /// <summary>
        /// Scoped service 1.
        /// This service is created once per HTTP request.
        /// </summary>
        private readonly IScopedService _scopedService1;

        /// <summary>
        /// Scoped service 2.
        /// Another instance of Scoped service for comparison within the same HTTP request.
        /// </summary>
        private readonly IScopedService _scopedService2;

        /// <summary>
        /// Transient service 1.
        /// A new instance of this service is created every time it is requested.
        /// </summary>
        private readonly ITransientService _transientService1;

        /// <summary>
        /// Transient service 2.
        /// Another instance of Transient service for comparison.
        /// </summary>
        private readonly ITransientService _transientService2;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLifetimeController"/> class.
        /// </summary>
        /// <param name="singletonService1">The first instance of the Singleton service.</param>
        /// <param name="singletonService2">The second instance of the Singleton service.</param>
        /// <param name="scopedService1">The first instance of the Scoped service.</param>
        /// <param name="scopedService2">The second instance of the Scoped service.</param>
        /// <param name="transientService1">The first instance of the Transient service.</param>
        /// <param name="transientService2">The second instance of the Transient service.</param>
        public ServiceLifetimeController(
            ISingletonService singletonService1,
            ISingletonService singletonService2,
            IScopedService scopedService1,
            IScopedService scopedService2,
            ITransientService transientService1,
            ITransientService transientService2)
        {
            // Assigning injected services to private fields for later use
            _singletonService1 = singletonService1;
            _singletonService2 = singletonService2;
            _scopedService1 = scopedService1;
            _scopedService2 = scopedService2;
            _transientService1 = transientService1;
            _transientService2 = transientService2;
        }

        /// <summary>
        /// Returns the operation ID for all registered services.
        /// This demonstrates how different service lifetimes impact instance creation.
        /// </summary>
        /// <returns>A response containing the operation IDs of Singleton, Scoped, and Transient services.</returns>
        [HttpGet]
        public IActionResult GetServiceLifetimes()
        {
            return Ok(new
            {
                // Singleton services: These should have the same operation ID
                Singleton = new
                {
                    Instance1 = _singletonService1.GetOperationId(),
                    Instance2 = _singletonService2.GetOperationId()
                },
                // Scoped services: These should have the same operation ID for the same HTTP request
                Scoped = new
                {
                    Instance1 = _scopedService1.GetOperationId(),
                    Instance2 = _scopedService2.GetOperationId()
                },
                // Transient services: These should have different operation IDs each time they are requested
                Transient = new
                {
                    Instance1 = _transientService1.GetOperationId(),
                    Instance2 = _transientService2.GetOperationId()
                }
            });
        }
    }
}