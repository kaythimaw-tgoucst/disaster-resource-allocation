using DisasterResourceAllocation.Application.UseCases.Areas.GetAllAreas;
using Microsoft.AspNetCore.Mvc;

namespace DisasterResourceAllocation.API.UseCase.Area
{
    [ApiController]
    [Route("api/areas")]
    public class GetAllAreasController : ControllerBase
    {
        private readonly IGetAllAreasUseCase _getAllAreasUseCase;

        public GetAllAreasController(IGetAllAreasUseCase getAllAreasUseCase)
        {
            _getAllAreasUseCase = getAllAreasUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAreas()
        {
            try
            {
                var areas = await _getAllAreasUseCase.ExecuteAsync();
                return Ok(areas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving areas: {ex.Message}");
            }
        }
    }
}
