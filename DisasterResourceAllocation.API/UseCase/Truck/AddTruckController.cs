using DisasterResourceAllocation.Application.DTOs;
using DisasterResourceAllocation.Application.UseCases.Trucks.AddTruck;
using Microsoft.AspNetCore.Mvc;

namespace DisasterResourceAllocation.API.UseCase.Truck
{
    [ApiController]
    [Route("api/trucks")]
    public class AddTruckController : ControllerBase
    {
        private readonly IAddTruckUseCase _addTruckUseCase;

        public AddTruckController(IAddTruckUseCase addTruckUseCase)
        {
            _addTruckUseCase = addTruckUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> AddTruck([FromBody] TruckDto truckDto)
        {
            if (string.IsNullOrEmpty(truckDto.TruckId))
                return BadRequest("Invalid truck data.");

            try
            {
                await _addTruckUseCase.ExecuteAsync(truckDto);
                return CreatedAtAction("GetAllTrucks", "GetAllTrucks", new { id = truckDto.TruckId }, truckDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding truck: {ex.Message}");
            }
        }
    }
}
