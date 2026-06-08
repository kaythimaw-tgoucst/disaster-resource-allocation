namespace DisasterResourceAllocation.Application.UseCases.Assignments.ProcessAssignments;

using DisasterResourceAllocation.Application.DTOs;
using DisasterResourceAllocation.Domain.Entities;
using DisasterResourceAllocation.Domain.Interfaces;

public class ProcessAssignmentsUseCase : IProcessAssignmentsUseCase
{
    private readonly IAssignmentRepository _repository;
    private readonly ICacheService _cacheService;
    private const string CACHE_KEY = "assignments_cache";

    public ProcessAssignmentsUseCase(IAssignmentRepository repository, ICacheService cacheService)
    {
        _repository = repository;
        _cacheService = cacheService;
    }

    public async Task<AssignmentResponseDto> ExecuteAsync()
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
        var hasTrucksWithResources = trucks.Any(t => HasSufficientResources(t, area));
        var hasTrucksThatCanReach = trucks.Any(t => CanReachInTime(t, area));

        if (!hasTrucksWithResources && !hasTrucksThatCanReach)
            return "No trucks available with sufficient resources and within time constraint";
        if (!hasTrucksWithResources)
            return "No trucks with sufficient resources available";
        if (!hasTrucksThatCanReach)
            return "No trucks can reach within time constraint";

        return "All suitable trucks already assigned";
    }
}