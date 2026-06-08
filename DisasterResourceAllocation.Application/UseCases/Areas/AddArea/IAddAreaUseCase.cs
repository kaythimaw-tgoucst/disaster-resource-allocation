namespace DisasterResourceAllocation.Application.UseCases.Areas.AddArea;

using DisasterResourceAllocation.Application.DTOs;

public interface IAddAreaUseCase
{
    Task ExecuteAsync(AreaDto areaDto);
}