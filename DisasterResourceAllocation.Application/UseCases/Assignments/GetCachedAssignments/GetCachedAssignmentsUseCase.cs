namespace DisasterResourceAllocation.Application.UseCases.Assignments.GetCachedAssignments;

using DisasterResourceAllocation.Application.DTOs;
using DisasterResourceAllocation.Domain.Interfaces;

public class GetCachedAssignmentsUseCase : IGetCachedAssignmentsUseCase
{
    private readonly ICacheService _cacheService;
    private const string CACHE_KEY = "assignments_cache";

    public GetCachedAssignmentsUseCase(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<AssignmentResponseDto?> ExecuteAsync()
    {
        return await _cacheService.GetAsync<AssignmentResponseDto>(CACHE_KEY);
    }
}