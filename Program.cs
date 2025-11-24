using Microsoft.EntityFrameworkCore;
using Nutra.Data;
using Nutra.Interfaces;
using Nutra.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=Data/alimentos.db";
builder.Services.AddDbContext<AlimentosContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<IBusca, BuscaService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
