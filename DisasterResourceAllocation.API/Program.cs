using DisasterResourceAllocation.Application.Services;
using DisasterResourceAllocation.Application.UseCases.Areas.AddArea;    
using DisasterResourceAllocation.Application.UseCases.Areas.GetAllAreas;
using DisasterResourceAllocation.Application.UseCases.Trucks.AddTruck;
using DisasterResourceAllocation.Application.UseCases.Trucks.GetAllTrucks;
using DisasterResourceAllocation.Application.UseCases.Assignments.ProcessAssignments;
using DisasterResourceAllocation.Application.UseCases.Assignments.GetCachedAssignments;
using DisasterResourceAllocation.Application.UseCases.Assignments.ClearAssignments;
using DisasterResourceAllocation.Domain.Interfaces;
using DisasterResourceAllocation.Infrastructure.Cache;
using DisasterResourceAllocation.Infrastructure.Repositories;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Redis configuration
var redisConnection = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(redisConnection)
);

// Dependency injection - Core Services
builder.Services.AddScoped<IAssignmentRepository, InMemoryAssignmentRepository>();
builder.Services.AddScoped<ICacheService, RedisCacheService>();
builder.Services.AddScoped<IAllocationService, AllocationService>();

// Dependency injection - Area Use Cases
builder.Services.AddScoped<IAddAreaUseCase, AddAreaUseCase>();
builder.Services.AddScoped<IGetAllAreasUseCase, GetAllAreasUseCase>();

// Dependency injection - Truck Use Cases
builder.Services.AddScoped<IAddTruckUseCase, AddTruckUseCase>();
builder.Services.AddScoped<IGetAllTrucksUseCase, GetAllTrucksUseCase>();

// Dependency injection - Assignment Use Cases
builder.Services.AddScoped<IProcessAssignmentsUseCase, ProcessAssignmentsUseCase>();
builder.Services.AddScoped<IGetCachedAssignmentsUseCase, GetCachedAssignmentsUseCase>();
builder.Services.AddScoped<IClearAssignmentsUseCase, ClearAssignmentsUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
