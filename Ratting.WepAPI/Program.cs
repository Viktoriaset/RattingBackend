using Ratting.Application.Common.Mappings;
using Ratting.Application.Interfaces;
using Ratting.Application;
using Ratting.Persistance;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Ratting.Aplication;
using Ratting.Persistance.Middleware;
using Ratting.WepAPI.Models.FindBattleModel;
using Ratting.WepAPI.Models.FinishBattleModel;
using Ratting.WepAPI.Models.UpdatePlayerModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
    });
builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new FindBattleDtoModelBinderProvider());
    options.ModelBinderProviders.Insert(1, new FinishBattleDtoModelBinderProvider());
    options.ModelBinderProviders.Insert(2, new UpdatePlayerDtoModelBinderProvider());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IRattingDBContext).Assembly));
});
builder.Services.AddHttpClient();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<RattingDbContext>();
        DBInitializer.Initialize(context);
    }
    catch (Exception exception)
    {

    }
}

app.UseCustomExceptionHandler();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
