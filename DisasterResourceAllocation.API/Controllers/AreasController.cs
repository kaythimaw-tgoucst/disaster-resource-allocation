namespace DisasterResourceAllocation.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using DisasterResourceAllocation.Application.DTOs;
using DisasterResourceAllocation.Application.Services;

[ApiController]
[Route("api/[controller]")]
public class AreasController : ControllerBase
{
    private readonly IAllocationService _allocationService;

    public AreasController(IAllocationService allocationService)
    {
        _allocationService = allocationService;
    }

    [HttpPost]
    public async Task<IActionResult> AddArea([FromBody] AreaDto areaDto)
    {
        if (string.IsNullOrEmpty(areaDto.AreaId) || areaDto.UrgencyLevel < 1 || areaDto.UrgencyLevel > 5)
            return BadRequest("Invalid area data. Urgency level must be between 1 and 5.");

        try
        {
            await _allocationService.AddAreaAsync(areaDto);
            return CreatedAtAction(nameof(GetAllAreas), new { id = areaDto.AreaId }, areaDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error adding area: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAreas()
    {
        try
        {
            var areas = await _allocationService.GetAllAreasAsync();
            return Ok(areas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving areas: {ex.Message}");
        }
    }
}
