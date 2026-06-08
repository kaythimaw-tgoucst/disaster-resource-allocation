namespace DisasterResourceAllocation.Application.UseCases.Trucks.AddTruck;

using DisasterResourceAllocation.Application.DTOs;

public interface IAddTruckUseCase
{
    Task ExecuteAsync(TruckDto truckDto);
}