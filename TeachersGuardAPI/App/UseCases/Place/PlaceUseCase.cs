using TeachersGuardAPI.App.DTOs.Place;
using TeachersGuardAPI.Domain.Repositories;
using TeachersGuardAPI.Infraestructure.Mappers;

namespace TeachersGuardAPI.App.UseCases.PlaceUseCase
{
    public class PlaceUseCase
    {
        private readonly IPlaceRepository _placeRepository;
        public PlaceUseCase(IPlaceRepository placeRepository) 
        {
            _placeRepository = placeRepository;
        }

        public async Task<PlaceDto?> GetPlaceByPlaceId(string placeId)
        {
            var place = await _placeRepository.GetPlaceByPlaceId(placeId);

            if (place == null) return null;

            return PlaceMapper.MapPlaceEntityToPlaceDto(place);
        }

        public async Task<List<PlaceDto>> GetPlaces()
        {
            var places = await _placeRepository.GetPlaces();

            return places.Select(PlaceMapper.MapPlaceEntityToPlaceDto).ToList();
        }

    }
}
