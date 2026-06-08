using DisasterResourceAllocation.Application.DTOs;
using DisasterResourceAllocation.Application.UseCases.Areas.AddArea;
using Microsoft.AspNetCore.Mvc;

namespace DisasterResourceAllocation.API.UseCase.Area
{
    [ApiController]
    [Route("api/areas")]
    public class AddAreaController : ControllerBase
    {
        private readonly IAddAreaUseCase _addAreaUseCase;

        public AddAreaController(IAddAreaUseCase addAreaUseCase)
        {
            _addAreaUseCase = addAreaUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> AddArea([FromBody] AreaDto areaDto)
        {
            if (string.IsNullOrEmpty(areaDto.AreaId) || areaDto.UrgencyLevel < 1 || areaDto.UrgencyLevel > 5)
                return BadRequest("Invalid area data. Urgency level must be between 1 and 5.");

            try
            {
                await _addAreaUseCase.ExecuteAsync(areaDto);
                return CreatedAtAction("GetAllAreas", "GetAllAreas", new { id = areaDto.AreaId }, areaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding area: {ex.Message}");
            }
        }
    }
}
