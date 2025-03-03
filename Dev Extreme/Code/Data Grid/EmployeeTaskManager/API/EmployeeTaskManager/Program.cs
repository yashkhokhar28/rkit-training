using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack;
using System.Configuration;
using EmployeeTaskManager.BL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

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

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeTaskManager API", Version = "v1" });

    // Define the JWT Bearer scheme
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token (e.g., 'Bearer {token}')",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Apply the security requirement globally to all endpoints
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

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
