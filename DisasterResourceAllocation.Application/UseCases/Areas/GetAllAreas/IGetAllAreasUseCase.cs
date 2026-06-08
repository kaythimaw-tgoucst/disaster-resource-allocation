namespace DisasterResourceAllocation.Application.UseCases.Areas.GetAllAreas;

using DisasterResourceAllocation.Application.DTOs;

public interface IGetAllAreasUseCase
{
    Task<List<AreaResponseDto>> ExecuteAsync();
}