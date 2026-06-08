namespace DisasterResourceAllocation.Application.UseCases.Areas.AddArea;

using DisasterResourceAllocation.Application.DTOs;
using DisasterResourceAllocation.Domain.Entities;
using DisasterResourceAllocation.Domain.Interfaces;

public class AddAreaUseCase : IAddAreaUseCase
{
    private readonly IAssignmentRepository _repository;

    public AddAreaUseCase(IAssignmentRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(AreaDto areaDto)
    {
        var area = new Area(
            areaDto.AreaId,
            areaDto.UrgencyLevel,
            areaDto.RequiredResources,
            areaDto.TimeConstraint
        );
        await _repository.AddAreaAsync(area);
    }
}