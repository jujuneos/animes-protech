using Animes.Infra.Data.Context;
using Animes.Infrastructure.IoC;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conex�o com o banco de dados
builder.Services.AddDbContext<AnimesDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AnimesConnection"));
});

// Refer�ncia ao container de depend�ncia
DependencyContainer.RegisterServices(builder.Services);

builder.Services.AddControllers();

builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
