using DependencyInjectionDemo.Extension;
using DependencyInjectionDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register OrderService with Scoped lifetime (created once per HTTP request)
builder.Services.AddScoped<OrderService>();

// Register PaymentService (PayPal or Stripe can be selected based on the need)
// Default is PayPalPaymentService
builder.Services.AddTransient<IPaymentService, PayPalPaymentService>(); 
// builder.Services.AddScoped<IPaymentService, StripePaymentService>(); 

// Register other services with appropriate lifetimes
builder.Services.AddSingleton<ISingletonService, SingletonService>();  // Singleton: one instance shared throughout the application
builder.Services.AddScoped<IScopedService, ScopedService>();  // Scoped: new instance created per HTTP request
builder.Services.AddTransient<ITransientService, TransientService>();  // Transient: new instance created every time it’s requested

// Register custom services using extension methods (e.g., AddProductServices)
builder.Services.AddProductServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
