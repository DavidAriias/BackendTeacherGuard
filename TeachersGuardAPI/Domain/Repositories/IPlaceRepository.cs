using TeachersGuardAPI.Domain.Entities;

namespace TeachersGuardAPI.Domain.Repositories
{
    public interface IPlaceRepository
    {
        public Task<Place?> GetPlaceByPlaceId(string placeId);
        public Task<List<Place>> GetPlaces();
    }
}
