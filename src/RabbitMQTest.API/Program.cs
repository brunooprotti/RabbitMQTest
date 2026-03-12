using Microsoft.EntityFrameworkCore;
using RabbitMQTest.API.Data;
using RabbitMQTest.API.Endpoints;
using RabbitMQTest.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("StudentDbContext");
builder.Services.AddDbContext<StudentDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IStudentDbContext, StudentDbContext>();

builder.Services.AddScoped<IMessageProducer, MessageProducer>(); // RabbitMQProducer

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapRabbitEndpoints();

app.Run();
