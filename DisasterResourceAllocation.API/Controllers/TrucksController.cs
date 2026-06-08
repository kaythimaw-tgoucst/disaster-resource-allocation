namespace DisasterResourceAllocation.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using DisasterResourceAllocation.Application.DTOs;
using DisasterResourceAllocation.Application.Services;

[ApiController]
[Route("api/[controller]")]
public class TrucksController : ControllerBase
{
    private readonly IAllocationService _allocationService;

    public TrucksController(IAllocationService allocationService)
    {
        _allocationService = allocationService;
    }

    [HttpPost]
    public async Task<IActionResult> AddTruck([FromBody] TruckDto truckDto)
    {
        if (string.IsNullOrEmpty(truckDto.TruckId))
            return BadRequest("Truck ID is required.");

        try
        {
            await _allocationService.AddTruckAsync(truckDto);
            return CreatedAtAction(nameof(GetAllTrucks), new { id = truckDto.TruckId }, truckDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error adding truck: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTrucks()
    {
        try
        {
            var trucks = await _allocationService.GetAllTrucksAsync();
            return Ok(trucks);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving trucks: {ex.Message}");
        }
    }
}
