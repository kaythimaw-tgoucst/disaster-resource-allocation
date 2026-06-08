namespace DisasterResourceAllocation.Infrastructure.Repositories;

using DisasterResourceAllocation.Domain.Entities;
using DisasterResourceAllocation.Domain.Interfaces;

public class InMemoryAssignmentRepository : IAssignmentRepository
{
    private static List<Area> _areas = new();
    private static List<Truck> _trucks = new();

    public Task AddAreaAsync(Area area)
    {
        _areas.Add(area);
        return Task.CompletedTask;
    }

    public Task AddTruckAsync(Truck truck)
    {
        _trucks.Add(truck);
        return Task.CompletedTask;
    }

    public Task<List<Area>> GetAllAreasAsync()
    {
        return Task.FromResult(_areas);
    }

    public Task<List<Truck>> GetAllTrucksAsync()
    {
        return Task.FromResult(_trucks);
    }

    public Task<Area?> GetAreaByIdAsync(string areaId)
    {
        var area = _areas.FirstOrDefault(a => a.AreaId == areaId);
        return Task.FromResult(area);
    }

    public Task<Truck?> GetTruckByIdAsync(string truckId)
    {
        var truck = _trucks.FirstOrDefault(t => t.TruckId == truckId);
        return Task.FromResult(truck);
    }

    public Task ClearAssignmentsAsync()
    {
        _areas.Clear();
        _trucks.Clear();
        return Task.CompletedTask;
    }
}
