namespace DisasterResourceAllocation.Domain.Interfaces;

using DisasterResourceAllocation.Domain.Entities;

public interface IAssignmentRepository
{
    Task AddAreaAsync(Area area);
    Task AddTruckAsync(Truck truck);
    Task<List<Area>> GetAllAreasAsync();
    Task<List<Truck>> GetAllTrucksAsync();
    Task<Area?> GetAreaByIdAsync(string areaId);
    Task<Truck?> GetTruckByIdAsync(string truckId);
    Task ClearAssignmentsAsync();
}
