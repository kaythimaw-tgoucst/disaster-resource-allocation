namespace DisasterResourceAllocation.Application.UseCases.Areas.GetAllAreas;

using DisasterResourceAllocation.Application.DTOs;
using DisasterResourceAllocation.Domain.Interfaces;

public class GetAllAreasUseCase : IGetAllAreasUseCase
{
    private readonly IAssignmentRepository _repository;

    public GetAllAreasUseCase(IAssignmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<AreaResponseDto>> ExecuteAsync()
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
}