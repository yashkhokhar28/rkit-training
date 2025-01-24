using LoggingDemo.Logging;
using LoggingDemo.Services;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure Serilog as a logging provider
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()  // Output logs to the console
    .WriteTo.File("logs/logfile.log", rollingInterval: RollingInterval.Day)  // Output logs to a file with daily rolling interval
    .CreateLogger();

builder.Host.UseSerilog();  // Set Serilog as the logging provider for the application

// Add logging services
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();    // Console logging provider
    logging.AddDebug();      // Debug logging provider
    logging.AddProvider(new CustomLoggerProvider()); // Custom logging provider
});

builder.Services.AddControllers();  // Add controller services to the container
builder.Services.AddTransient<SampleService>(); // Register SampleService with transient lifetime

builder.Services.AddEndpointsApiExplorer();  // Add API explorer for endpoint discovery
builder.Services.AddSwaggerGen();  // Add Swagger generator for API documentation

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Enable Swagger middleware to serve generated Swagger as a JSON endpoint
    app.UseSwaggerUI();  // Enable Swagger UI
}

app.UseAuthorization();  // Add authorization middleware to the request pipeline

app.MapControllers();  // Map controller routes

app.Run();  // Run the application