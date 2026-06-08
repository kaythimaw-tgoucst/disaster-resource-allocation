using DisasterResourceAllocation.Application.UseCases.Assignments.GetCachedAssignments;
using Microsoft.AspNetCore.Mvc;

namespace DisasterResourceAllocation.API.UseCase.Assignment
{
    [ApiController]
    [Route("api/assignments")]
    public class GetCachedAssignmentsController : ControllerBase
    {
        private readonly IGetCachedAssignmentsUseCase _getCachedAssignmentsUseCase;

        public GetCachedAssignmentsController(IGetCachedAssignmentsUseCase getCachedAssignmentsUseCase)
        {
            _getCachedAssignmentsUseCase = getCachedAssignmentsUseCase;
        }

        [HttpGet("cached")]
        public async Task<IActionResult> GetCachedAssignments()
        {
            try
            {
                var result = await _getCachedAssignmentsUseCase.ExecuteAsync();
                if (result == null)
                    return NotFound("No cached assignments found");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving cached assignments: {ex.Message}");
            }
        }
    }
}
