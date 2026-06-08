using DisasterResourceAllocation.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DisasterResourceAllocation.API.UseCase.Assignment
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteAssignmentController : ControllerBase
    {
        private readonly IAllocationService _allocationService;

        public DeleteAssignmentController(IAllocationService allocationService)
        {
            _allocationService = allocationService;
        }

        [HttpDelete]
        public async Task<IActionResult> ClearAssignments()
        {
            try
            {
                await _allocationService.ClearAssignmentsAsync();
                return Ok("Assignments cleared successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error clearing assignments: {ex.Message}");
            }
        }
    }
}
