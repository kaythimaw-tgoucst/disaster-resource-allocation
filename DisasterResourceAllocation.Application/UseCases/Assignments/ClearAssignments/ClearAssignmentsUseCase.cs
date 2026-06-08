namespace DisasterResourceAllocation.Application.UseCases.Assignments.ClearAssignments;

using DisasterResourceAllocation.Domain.Interfaces;

public class ClearAssignmentsUseCase : IClearAssignmentsUseCase
{
    private readonly IAssignmentRepository _repository;
    private readonly ICacheService _cacheService;
    private const string CACHE_KEY = "assignments_cache";

    public ClearAssignmentsUseCase(IAssignmentRepository repository, ICacheService cacheService)
    {
        _repository = repository;
        _cacheService = cacheService;
    }

    public async Task ExecuteAsync()
    {
        await _cacheService.RemoveAsync(CACHE_KEY);
        await _repository.ClearAssignmentsAsync();
    }
}