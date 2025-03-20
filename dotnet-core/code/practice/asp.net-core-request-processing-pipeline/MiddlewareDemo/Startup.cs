using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MiddlewareDemo
{
    public class Startup
    {
        // Register services in the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add controllers
            services.AddControllers();

            // Add Swagger for API documentation
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Configure CORS policies (example configuration)
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.WithOrigins("https://mywebsite.com")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            //Add authentication and authorization if needed
             services.AddAuthentication("Bearer")
                     .AddJwtBearer(options =>
                     {
                         options.Authority = "https://your-auth-provider.com";
                         options.Audience = "your-api";
                     });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });
        }

        // Configure middleware pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Use Swagger in the development environment
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Use static files middleware
            app.UseStaticFiles();

            // Add custom inline middleware for logging
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
                await next();
                Console.WriteLine($"Response: {context.Response.StatusCode}");
            });

            // Use global exception handler middleware
            app.UseExceptionHandler("/error");

            // Use CORS middleware
            app.UseCors("AllowSpecificOrigin");

            // Use authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Enable routing
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
