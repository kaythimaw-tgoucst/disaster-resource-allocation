namespace DisasterResourceAllocation.Application.UseCases.Trucks.AddTruck;

using DisasterResourceAllocation.Application.DTOs;
using DisasterResourceAllocation.Domain.Entities;
using DisasterResourceAllocation.Domain.Interfaces;

public class AddTruckUseCase : IAddTruckUseCase
{
    private readonly IAssignmentRepository _repository;

    public AddTruckUseCase(IAssignmentRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(TruckDto truckDto)
    {
        var truck = new Truck(
            truckDto.TruckId,
            truckDto.AvailableResources,
            truckDto.TravelTimes
        );
        await _repository.AddTruckAsync(truck);
    }
}