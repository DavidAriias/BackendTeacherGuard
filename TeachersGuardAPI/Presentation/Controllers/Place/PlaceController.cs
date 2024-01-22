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
            if (placeId == null) return BadRequest("El placeId debe ser dado");

            var place = await _placeUseCase.GetPlaceByPlaceId(placeId);

            return place != null ? Ok(new { Place = place }) : NotFound(new { Message = "El id proporcionado no fue encontrado" });
        }

        [HttpGet("get-places")]
        public async Task<ActionResult<List<PlaceDto>>> GetPlaces()
        {   
            var places = await _placeUseCase.GetPlaces();

            return Ok(new { Places = places});
        }




    }
}
