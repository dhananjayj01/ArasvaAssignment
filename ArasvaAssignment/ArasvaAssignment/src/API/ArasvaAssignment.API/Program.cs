using ArasvaAssignment.API.Extensions;
using ArasvaAssignment.Application;
using ArasvaAssignment.Identity;
using ArasvaAssignment.Persistence;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInterfaceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddIdentityServices();
builder.Services.AddControllers();
builder.Services.AddControllers()
       .ConfigureCustomValidationResponse();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ArasvaAssignment",
        Version = "v1"
    });
});
 
var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI(); 
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => Results.Redirect("/swagger"))
   .ExcludeFromDescription();
app.Run();
    