namespace DisasterResourceAllocation.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using DisasterResourceAllocation.Application.Services;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    private readonly IAllocationService _allocationService;

    public AssignmentsController(IAllocationService allocationService)
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
