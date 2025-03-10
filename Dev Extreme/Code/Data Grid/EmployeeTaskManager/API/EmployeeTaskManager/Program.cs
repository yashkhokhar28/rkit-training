using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack;
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
    // Define a CORS policy named "EmployeeTaskManagerGUI" for cross-origin requests
    options.AddPolicy("EmployeeTaskManagerGUI", policy =>
        policy.WithOrigins("http://127.0.0.1:5507") // Allow requests from this specific origin (local development GUI)
              .AllowAnyMethod()                     // Permit all HTTP methods (GET, POST, etc.)
              .AllowAnyHeader());                   // Permit all request headers
});

// Add MVC controllers with Newtonsoft.Json support for JSON serialization
builder.Services.AddControllers().AddNewtonsoftJson();

// Register the database connection factory as a singleton service
builder.Services.AddSingleton<IDbConnectionFactory>(
    new OrmLiteConnectionFactory(
        builder.Configuration.GetConnectionString("EmployeeTaskManager"), // Connection string from configuration
        MySqlDialect.Provider)                                          // Use MySQL dialect for ORM Lite
);

// Configure Swagger for API documentation
builder.Services.AddSwaggerGen(options =>
{
    // Define Swagger document metadata
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeTaskManager API", Version = "v1" });

    // Define JWT Bearer authentication scheme for Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,          // Token is passed in the Authorization header
        Description = "Please enter a valid token (e.g., 'Bearer {token}')",
        Name = "Authorization",                 // Header name for the token
        Type = SecuritySchemeType.Http,         // HTTP-based authentication
        Scheme = "bearer",                      // Bearer scheme
        BearerFormat = "JWT"                    // Token format is JWT
    });

    // Apply JWT authentication requirement globally to all endpoints
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
            new string[] { }  // No additional scopes required
        }
    });
});

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Define token validation parameters
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,                            // Validate the token issuer
            ValidateAudience = true,                          // Validate the token audience
            ValidateLifetime = true,                          // Ensure the token hasn't expired
            ValidateIssuerSigningKey = true,                  // Validate the signing key
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Expected issuer from configuration
            ValidAudience = builder.Configuration["Jwt:Audience"], // Expected audience from configuration
            IssuerSigningKey = new SymmetricSecurityKey(      // Signing key for token verification
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Configure Authorization Policies
builder.Services.AddAuthorization(options =>
{
    // Policy for Admin-only access
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    // Policy for Manager or Admin access
    options.AddPolicy("ManagerOrAdmin", policy => policy.RequireRole("Admin", "Manager"));
    // Policy for Employee access
    options.AddPolicy("Employee", policy => policy.RequireRole("Employee"));
});

// Register business logic services with scoped lifetime
builder.Services.AddScoped<BLTask>();        // Task management service
builder.Services.AddScoped<BLDepartment>();  // Department management service
builder.Services.AddScoped<BLAuth>();        // Authentication and user management service

// Add API explorer and Swagger generation services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI in development environment for API testing and documentation
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS globally using the defined policy
app.UseCors("EmployeeTaskManagerGUI"); // Apply the CORS policy for GUI access

// Configure the HTTP request pipeline
app.UseAuthentication(); // Enable JWT authentication middleware
app.UseAuthorization();  // Enable authorization middleware

// Map controller endpoints
app.MapControllers();

app.Run(); // Start the application