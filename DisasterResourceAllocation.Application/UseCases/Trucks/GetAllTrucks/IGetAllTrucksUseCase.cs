namespace DisasterResourceAllocation.Application.UseCases.Trucks.GetAllTrucks;

using DisasterResourceAllocation.Application.DTOs;

public interface IGetAllTrucksUseCase
{
    Task<List<TruckResponseDto>> ExecuteAsync();
}