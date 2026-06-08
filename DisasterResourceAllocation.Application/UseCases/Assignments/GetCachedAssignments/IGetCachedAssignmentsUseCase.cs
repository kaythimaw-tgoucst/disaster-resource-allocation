namespace DisasterResourceAllocation.Application.UseCases.Assignments.GetCachedAssignments;

using DisasterResourceAllocation.Application.DTOs;

public interface IGetCachedAssignmentsUseCase
{
    Task<AssignmentResponseDto?> ExecuteAsync();
}