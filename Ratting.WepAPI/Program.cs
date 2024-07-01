using Ratting.Aplication.Common.Mappings;
using Ratting.Aplication.Interfaces;
using Ratting.Aplication;
using Ratting.Persistance;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Ratting.Persistance.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IRattingDBContext).Assembly));
});
builder.Services.AddHttpClient();
builder.Services.AddAplication();
builder.Services.AddPersistence(builder.Configuration);
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

/*// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

//app.UseHttpsRedirection();
app.UseCustomExceptionHandler();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
