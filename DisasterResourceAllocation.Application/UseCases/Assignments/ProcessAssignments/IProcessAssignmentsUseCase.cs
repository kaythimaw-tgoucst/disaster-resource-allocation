namespace DisasterResourceAllocation.Application.UseCases.Assignments.ProcessAssignments;

using DisasterResourceAllocation.Application.DTOs;

public interface IProcessAssignmentsUseCase
{
    Task<AssignmentResponseDto> ExecuteAsync();
}