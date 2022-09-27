using Hotel_listing.API;
using Hotel_listing.API.Common;
using Hotel_listing.API.Middleware;
using Hotel_listing.Application.Common;
using Hotel_listing.Infrastructure.Extensions;
using Hotel_listing.Persistence.Contexts;
using Hotel_listing.Persistence.Extensions;
using Hotel_listing.Logger.Common.Configs;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration().CreateLogger();

try
{
    Log.Information(API_Const.APP_STARTED);
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
        Log.Fatal(e,API_Const.MIGRATION_ERROR);
    }
    #endregion

    #region HTTP request pipeline (Middlewares)

    app.UseMiddleware<ExceptionMiddleware>();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseCors(API_Const.CORS_POLICY);
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
    #endregion
}
catch (Exception e)
{
    Log.Fatal(e, API_Const.APP_STOPED_WORKING);
}
finally
{
    Log.Information(API_Const.APP_SHUT_DOWN);
    Log.CloseAndFlush();
}