namespace DisasterResourceAllocation.Application.Services;

using DisasterResourceAllocation.Application.DTOs;

public interface IAllocationService
{
    Task AddAreaAsync(AreaDto areaDto);
    Task AddTruckAsync(TruckDto truckDto);
    Task<AssignmentResponseDto> ProcessAssignmentsAsync();
    Task<AssignmentResponseDto?> GetCachedAssignmentsAsync();
    Task ClearAssignmentsAsync();
    Task<List<AreaResponseDto>> GetAllAreasAsync();
    Task<List<TruckResponseDto>> GetAllTrucksAsync();
}
