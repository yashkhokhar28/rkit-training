using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack;
using System.Configuration;
using EmployeeTaskManager.BL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("EmployeeTaskManagerGUI", policy =>
        policy.WithOrigins("http://127.0.0.1:5505")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(builder.Configuration.GetConnectionString("EmployeeTaskManager"), MySqlDialect.Provider));

// JWT Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("ManagerOrAdmin", policy => policy.RequireRole("Admin", "Manager"));
    options.AddPolicy("Employee", policy => policy.RequireRole("Employee"));
});

builder.Services.AddScoped<BLTask>();
builder.Services.AddScoped<BLEmployee>();
builder.Services.AddScoped<BLDepartment>();
builder.Services.AddScoped<BLAuth>();


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


// Configure the HTTP request pipeline
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
