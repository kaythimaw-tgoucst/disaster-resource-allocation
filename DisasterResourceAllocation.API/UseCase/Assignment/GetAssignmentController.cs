using DisasterResourceAllocation.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DisasterResourceAllocation.API.UseCase.Assignment
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAssignmentController : ControllerBase
    {
        private readonly IAllocationService _allocationService;

        public GetAssignmentController(IAllocationService allocationService)
        {
            _allocationService = allocationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssignments()
        {
            try
            {
                var assignments = await _allocationService.GetCachedAssignmentsAsync();
                if (assignments == null)
                    return NotFound("No cached assignments found. Please process assignments first.");

                return Ok(assignments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving assignments: {ex.Message}");
            }
        }
    }
}
