using Microsoft.AspNetCore.Mvc;
using TeachersGuardAPI.App.DTOs.Place;
using TeachersGuardAPI.App.UseCases.PlaceUseCase;

namespace TeachersGuardAPI.Presentation.Controllers.Place
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly PlaceUseCase _placeUseCase;
        public PlaceController(PlaceUseCase placeUseCase) 
        {
            _placeUseCase = placeUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<PlaceDto?>> GetPlaceByPlaceId(string placeId)
        {
            if (placeId == null) return BadRequest("placeId must be provided");

            var place = await _placeUseCase.GetPlaceByPlaceId(placeId);

            return place != null ? Ok(place) : NotFound(new { Message = "This placeId maybe doesn't exits" });
        }


    }
}
