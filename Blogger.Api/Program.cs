using Infrastructure.Seeders;
using Infrastructure.Extensions;
using Application.Extensions;
using Application.Middleware;
using System.Text.Json.Serialization;
using Blogger.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opt.JsonSerializerOptions.Converters.Add(new CustomDateFormatConverter());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

// Add infrastructure services extension 
builder.Services.AddInfrastructure(builder.Configuration);

// Add application services extension
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

var scope = app.Services.CreateScope();

// Add post seeder
var seeder = scope.ServiceProvider.GetRequiredService<BloggerSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
