using DisasterResourceAllocation.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DisasterResourceAllocation.API.UseCase.Assignment
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddAssignmentController : ControllerBase
    {
        private readonly IAllocationService _allocationService;

        public AddAssignmentController(IAllocationService allocationService)
        {
            _allocationService = allocationService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessAssignments()
        {
            try
            {
                var assignments = await _allocationService.ProcessAssignmentsAsync();
                return Ok(assignments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing assignments: {ex.Message}");
            }
        }
    }
}
