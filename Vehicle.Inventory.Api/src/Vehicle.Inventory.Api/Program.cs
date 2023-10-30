using Microsoft.EntityFrameworkCore;
using Vehicle.Inventory.Api;
using Vehicle.Inventory.Core.Contracts;
using Vehicle.Inventory.Infrastructure.Data;
using Vehicle.Inventory.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(c => 
        c.RegisterServicesFromAssemblyContaining<StartingAssemblyMarkerClass>());

builder.Services.AddDbContext<ApplicationContext>(options =>
        options.UseInMemoryDatabase("TestDb"));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ITrucksPersistenceRepository, TrucksPersistenceRepository>();

builder.Services.AddScoped<ITrucksReadModelRepository, TrucksReadModelRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
