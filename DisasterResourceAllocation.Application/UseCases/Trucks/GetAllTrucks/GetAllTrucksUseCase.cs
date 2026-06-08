namespace DisasterResourceAllocation.Application.UseCases.Trucks.GetAllTrucks;

using DisasterResourceAllocation.Application.DTOs;
using DisasterResourceAllocation.Domain.Interfaces;

public class GetAllTrucksUseCase : IGetAllTrucksUseCase
{
    private readonly IAssignmentRepository _repository;

    public GetAllTrucksUseCase(IAssignmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TruckResponseDto>> ExecuteAsync()
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
}