using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using MiddlewareDemo.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure for authentication 

// builder.Services.AddAuthentication("Bearer")
//     .AddJwtBearer(options =>
//     {
//         options.Authority = "https://your-auth-provider.com"; // e.g., Auth0, IdentityServer
//         options.Audience = "your-api";  // The API's identifier
//     });

// Configure for authorization 

// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
// });

// Configure CORS 

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowSpecificOrigin", policy =>
//     {
//         policy.WithOrigins("https://mywebsite.com")  // Allow only mywebsite.com
//               .AllowAnyHeader()
//               .AllowAnyMethod();
//     });
// });

// Add Swagger services for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use the Short-Circuit middleware 
// Short-circuiting middleware checks conditions and stops request processing early.

// app.UseMiddleware<ShortCircuitMiddleware>();

// Calling middleware using a custom middleware class (Logging Middleware)

// app.UseMiddleware<LoggingMiddleware>();

// Calling middleware using Extension method (Request Logging Middleware)
// This is an alternate way to add the logging middleware to the pipeline.

// app.UseRequestLogging();

// Calling middleware using Inline Middleware (Lambda-based)
// Inline middleware is a quick, one-off middleware definition using lambda expressions.
// It logs request and response information directly within the pipeline.

// app.Use(async (context, next) =>
// {
//     Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
//     await next();
//     Console.WriteLine($"Response: {context.Response.StatusCode}");
// });

// Calling middleware using Chaining Middleware
// In this case, we have two chained middleware components. Each one logs before and after the next middleware runs.

// app.Use(async (context, next) =>
// {
//     Console.WriteLine("Middleware 1 - Before");
//     await next();
//     Console.WriteLine("Middleware 1 - After");
// });

// app.Use(async (context, next) =>
// {
//     Console.WriteLine("Middleware 2 - Before");
//     await next();
//     Console.WriteLine("Middleware 2 - After");
// });

// Calling middleware using Terminal Middleware
// Terminal middleware is used to end the request processing pipeline, stopping the execution of subsequent middlewares.

// app.Run(async context =>
// {
//     await context.Response.WriteAsync("This is terminal middleware. No further middleware will execute.");
// });

// Use Static Files Middleware 
// If your application serves static content (e.g., images, CSS, JavaScript files), use this middleware.

// app.UseStaticFiles();

// Use routing and map the controllers (this part is essential for routing)
app.UseRouting();    // Enables routing to controllers or endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();  // Maps controller routes to the HTTP request
});

// Global exception handler middleware 
// This middleware will catch unhandled exceptions in the request pipeline and return a consistent error response.

// app.UseExceptionHandler("/error"); // Redirects to the error handler endpoint

// Optionally, you can use an inline error handler
// This inline handler shows a problem message when an exception is caught.
// app.Map("/error", (HttpContext context) =>
// {
//     var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//     return Results.Problem(title: "An error occurred", detail: exception?.Message);
// });

// Swagger UI (only in development environment)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Generates the Swagger JSON
    app.UseSwaggerUI();  // Displays the Swagger UI for interacting with the API documentation
}

// Authentication Middleware 
// If authentication is enabled, uncomment this to ensure users are authenticated before accessing protected resources
// app.UseAuthentication();

// Authorization Middleware (enabled)
app.UseAuthorization();  // Ensures that the authenticated user has the necessary permissions to access the requested resource

// Enable CORS middleware 
// app.UseCors();  // Apply the CORS policy

// Map controllers (essential for routing and serving API endpoints)
app.MapControllers();

app.Run();
