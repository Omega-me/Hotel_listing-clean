using Hotel_listing.Application.Configurations;
using Hotel_listing.Infrastructure.DatabaseManager.Context;
using Hotel_listing.Presantation.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration().CreateLogger();

try
{
    Log.Information("Application starting...");
    var builder = WebApplication.CreateBuilder(args);
    LoggerConfig.LoggerConnector(builder);

    #region Container services
    builder.Services.ConfigureRepository();
    builder.Services.ConfigureControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.ConfigureCors();
    builder.Services.ConfigureDbContext(builder.Configuration);
    builder.Services.ConfigureSwagger();
    builder.Services.AddAutoMapper(typeof(MapperConfig));
    builder.Services.ConfigureApiBehavior();
    #endregion

    var app = builder.Build();

    #region Database migration
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<DatabaseContext>();
        dbContext.Database.Migrate();
    }
    catch (Exception e)
    {
        Log.Fatal(e,"An error accured during migrations");
    }
    #endregion

    #region HTTP request pipeline (Middlewares)
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseCors("CorsPolicy");
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
    #endregion
}
catch (Exception e)
{
    Log.Fatal(e, "Application stopped working");
}
finally
{
    Log.Information("Application is shut down");
    Log.CloseAndFlush();
}