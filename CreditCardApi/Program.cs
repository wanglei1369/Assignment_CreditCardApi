using CreditCardApi.Data;
using CreditCardApi.Endpoints;
using CreditCardApi.Interface;
using CreditCardApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source = CreditCard.db"));

builder.Services.AddScoped<ICreditCardService, CreditCardService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Register all endpoint modules
app.MapCreditCardEndpoints();

app.Run();
