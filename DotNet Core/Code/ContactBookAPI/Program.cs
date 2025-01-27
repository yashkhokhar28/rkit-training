using ContactBookAPI.BL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register the configuration to read connection strings
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddControllers().AddNewtonsoftJson();

// Initialize the BLConnection class
BLConnection.Initialize(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
