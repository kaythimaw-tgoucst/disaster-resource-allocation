namespace DisasterResourceAllocation.Application.Services;

using DisasterResourceAllocation.Application.DTOs;
using DisasterResourceAllocation.Domain.Entities;
using DisasterResourceAllocation.Domain.Interfaces;

public class AllocationService : IAllocationService
{
    private readonly IAssignmentRepository _repository;
    private readonly ICacheService _cacheService;
    private const string CACHE_KEY = "assignments_cache";

    public AllocationService(IAssignmentRepository repository, ICacheService cacheService)
    {
        _repository = repository;
        _cacheService = cacheService;
    }

    public async Task AddAreaAsync(AreaDto areaDto)
    {
        var area = new Area(
            areaDto.AreaId,
            areaDto.UrgencyLevel,
            areaDto.RequiredResources,
            areaDto.TimeConstraint
        );
        await _repository.AddAreaAsync(area);
    }

    public async Task AddTruckAsync(TruckDto truckDto)
    {
        var truck = new Truck(
            truckDto.TruckId,
            truckDto.AvailableResources,
            truckDto.TravelTimes
        );
        await _repository.AddTruckAsync(truck);
    }

    public async Task<AssignmentResponseDto> ProcessAssignmentsAsync()
    {
        var areas = await _repository.GetAllAreasAsync();
        var trucks = await _repository.GetAllTrucksAsync();

        // Sort areas by urgency (highest first)
        var sortedAreas = areas.OrderByDescending(a => a.UrgencyLevel).ToList();

        var assignments = new List<AssignmentDto>();
        var usedTrucks = new HashSet<string>();

        foreach (var area in sortedAreas)
        {
            var assignment = new AssignmentDto { AreaId = area.AreaId };
            var assignedTruck = FindSuitableTruck(area, trucks, usedTrucks);

            if (assignedTruck != null)
            {
                assignment.TruckId = assignedTruck.TruckId;
                assignment.ResourcesDelivered = assignedTruck.AvailableResources
                    .Where(r => area.RequiredResources.ContainsKey(r.Key))
                    .ToDictionary(r => r.Key, r => Math.Min(r.Value, area.RequiredResources[r.Key]));
                assignment.Status = "Assigned";
                usedTrucks.Add(assignedTruck.TruckId);
            }
            else
            {
                assignment.Status = "Failed";
                assignment.FailureReason = DetermineFailureReason(area, trucks);
            }

            assignments.Add(assignment);
        }

        var response = new AssignmentResponseDto
        {
            Assignments = assignments,
            GeneratedAt = DateTime.UtcNow,
            TotalAssigned = assignments.Count(a => a.Status == "Assigned"),
            TotalFailed = assignments.Count(a => a.Status == "Failed")
        };

        // Cache results for 30 minutes
        await _cacheService.SetAsync(CACHE_KEY, response, TimeSpan.FromMinutes(30));

        return response;
    }

    public async Task<AssignmentResponseDto?> GetCachedAssignmentsAsync()
    {
        return await _cacheService.GetAsync<AssignmentResponseDto>(CACHE_KEY);
    }

    public async Task ClearAssignmentsAsync()
    {
        await _cacheService.RemoveAsync(CACHE_KEY);
        await _repository.ClearAssignmentsAsync();
    }

    public async Task<List<AreaResponseDto>> GetAllAreasAsync()
    {
        var areas = await _repository.GetAllAreasAsync();
        return areas.Select(a => new AreaResponseDto
        {
            AreaId = a.AreaId,
            UrgencyLevel = a.UrgencyLevel,
            RequiredResources = a.RequiredResources,
            TimeConstraint = a.TimeConstraint,
            CreatedAt = a.CreatedAt
        }).ToList();
    }

    public async Task<List<TruckResponseDto>> GetAllTrucksAsync()
    {
        var trucks = await _repository.GetAllTrucksAsync();
        return trucks.Select(t => new TruckResponseDto
        {
            TruckId = t.TruckId,
            AvailableResources = t.AvailableResources,
            TravelTimes = t.TravelTimes,
            IsAssigned = t.IsAssigned,
            CreatedAt = t.CreatedAt
        }).ToList();
    }

    private Truck? FindSuitableTruck(Area area, List<Truck> trucks, HashSet<string> usedTrucks)
    {
        return trucks
            .Where(t => !usedTrucks.Contains(t.TruckId))
            .Where(t => CanReachInTime(t, area))
            .Where(t => HasSufficientResources(t, area))
            .FirstOrDefault();
    }

    private bool CanReachInTime(Truck truck, Area area)
    {
        if (!truck.TravelTimes.TryGetValue(area.AreaId, out var travelTime))
            return false;

        return travelTime <= area.TimeConstraint;
    }

    private bool HasSufficientResources(Truck truck, Area area)
    {
        foreach (var (resourceType, needed) in area.RequiredResources)
        {
            if (!truck.AvailableResources.TryGetValue(resourceType, out var available) || available < needed)
                return false;
        }
        return true;
    }

    private string DetermineFailureReason(Area area, List<Truck> trucks)
    {
        var availableTrucks = trucks.Where(t => t.TravelTimes.ContainsKey(area.AreaId)).ToList();

        if (!availableTrucks.Any())
            return "No trucks available with travel time information for this area.";

        var trucksTooFar = availableTrucks.Where(t => t.TravelTimes[area.AreaId] > area.TimeConstraint).Count();
        if (trucksTooFar == availableTrucks.Count)
            return $"All available trucks cannot reach the area within the {area.TimeConstraint} hour time constraint.";

        return "Insufficient resources available from any truck that can reach in time.";
    }
}
