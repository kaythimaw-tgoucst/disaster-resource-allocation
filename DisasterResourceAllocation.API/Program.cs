using DisasterResourceAllocation.Application.Services;
using DisasterResourceAllocation.Domain.Interfaces;
using DisasterResourceAllocation.Infrastructure.Cache;
using DisasterResourceAllocation.Infrastructure.Repositories;
using StackExchange.Redis;

var builder = WebApplicationBuilder.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Redis configuration
var redisConnection = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(redisConnection)
);

// Dependency injection
builder.Services.AddScoped<IAssignmentRepository, InMemoryAssignmentRepository>();
builder.Services.AddScoped<ICacheService, RedisCacheService>();
builder.Services.AddScoped<IAllocationService, AllocationService>();

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
