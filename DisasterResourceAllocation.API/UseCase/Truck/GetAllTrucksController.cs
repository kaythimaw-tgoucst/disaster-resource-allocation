using DisasterResourceAllocation.Application.UseCases.Trucks.GetAllTrucks;
using Microsoft.AspNetCore.Mvc;

namespace DisasterResourceAllocation.API.UseCase.Truck
{
    [ApiController]
    [Route("api/trucks")]
    public class GetAllTrucksController : ControllerBase
    {
        private readonly IGetAllTrucksUseCase _getAllTrucksUseCase;

        public GetAllTrucksController(IGetAllTrucksUseCase getAllTrucksUseCase)
        {
            _getAllTrucksUseCase = getAllTrucksUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrucks()
        {
            try
            {
                var trucks = await _getAllTrucksUseCase.ExecuteAsync();
                return Ok(trucks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving trucks: {ex.Message}");
            }
        }
    }
}
