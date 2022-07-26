using Hotel_listing.Application.Configurations;
using Hotel_listing.Presantation.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration().CreateLogger();

try
{
    Log.Information("Application starting...");
    var builder = WebApplication.CreateBuilder(args);
    LoggerConfig.LoggerConnector(builder);

    #region Container services
    builder.Services.ConfigureRepository();
    builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.ConfigureCors();
    builder.Services.ConfigureDbContext(builder.Configuration);
    builder.Services.ConfigureSwagger();
    builder.Services.AddAutoMapper(typeof(MapperConfig));
    #endregion

    var app = builder.Build();

    #region HTTP request pipeline (Middlewares)
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
    #endregion
}
catch (Exception ex)
{
    Log.Fatal(ex, "Aplication stoped working");
}
finally
{
    Log.Information("Application is shut down");
    Log.CloseAndFlush();
}