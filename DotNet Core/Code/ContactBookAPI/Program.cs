using ContactBookAPI.BL;
using ContactBookAPI.Filters;
using ContactBookAPI.Models;
using NLog.Web;
using System;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    // Register the configuration to read connection strings
    builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
    builder.Services.AddTransient<BLContactBook>();
    builder.Services.AddTransient<Response>();
    builder.Services.AddTransient<BLConverter>();
    builder.Services.AddTransient<CustomValidationFilter>();
;

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


