using FiltersDemo.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAuthorization(); // Add authorization services
builder.Services.AddAuthentication(); // Add authentication services if needed

// Register custom filters
builder.Services.AddScoped<CustomAuthorizationFilter>();
builder.Services.AddScoped<CustomExceptionFilter>();
builder.Services.AddScoped<CustomActionFilter>();
builder.Services.AddScoped<CustomResourceFilter>();
builder.Services.AddScoped<CustomResultFilter>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization(); // Middleware for authorization

app.MapControllers();

app.Run();
