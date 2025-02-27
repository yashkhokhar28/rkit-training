using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack;
using System.Configuration;
using EmployeeTaskManager.BL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(builder.Configuration.GetConnectionString("EmployeeTaskManager"), MySqlDialect.Provider));

builder.Services.AddScoped<BLTask>();


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
