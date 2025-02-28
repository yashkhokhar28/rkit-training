using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack;
using System.Configuration;
using EmployeeTaskManager.BL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("EmployeeTaskManagerGUI", policy =>
        policy.WithOrigins("http://127.0.0.1:5500")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(builder.Configuration.GetConnectionString("EmployeeTaskManager"), MySqlDialect.Provider));

builder.Services.AddScoped<BLTask>();
builder.Services.AddScoped<BLEmployee>();
builder.Services.AddScoped<BLDepartment>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS globally
app.UseCors("EmployeeTaskManagerGUI"); // Use the correct policy name

app.UseAuthorization();

app.MapControllers();

app.Run();
