using ContactBookAPI.BL;
using ContactBookAPI.Filters;
using NLog.Web;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
    builder.Services.AddTransient<CustomValidationFilter>();

    // Add CORS Policy
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("SpecificOrigin", policy =>
            policy.WithOrigins("http://127.0.0.1:5500") // Allow only this specific origin
                  .AllowAnyMethod()
                  .AllowAnyHeader());
    });


    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<CustomValidationFilter>();
    }).AddNewtonsoftJson();

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
        app.UseDeveloperExceptionPage();
    }

    // Enable CORS globally
    app.UseCors("SpecificOrigin"); // Use the correct policy name

    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
