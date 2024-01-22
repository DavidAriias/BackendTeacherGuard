using MongoDB.Bson;
using MongoDB.Driver;
using TeachersGuardAPI.Domain.Entities;
using TeachersGuardAPI.Domain.Repositories;
using TeachersGuardAPI.Infraestructure.Mappers;
using TeachersGuardAPI.Infraestructure.Models.Mongo;
using TeachersGuardAPI.Infraestructure.Persistence.Context;

namespace TeachersGuardAPI.Infraestructure.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly MongoContext _context;
        private readonly ILogger<PlaceRepository> _logger;
        public PlaceRepository(MongoContext context, ILogger<PlaceRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Place?> GetPlaceByPlaceId(string placeId)
        {
            _logger.LogInformation("Finding place with placeId: " + placeId);

            var filter = Builders<PlaceDocument>.Filter.Eq(u => u.Id, ObjectId.Parse(placeId));

            var placeDocument = await _context.Places.Find(filter).FirstOrDefaultAsync();

            if (placeDocument != null) 
            {
                return PlaceMapper.MapPlaceDocumentToPlaceEntity(placeDocument);
            }

            return null;
        }

        public async Task<List<Place>> GetPlaces()
        {
            _logger.LogInformation("Finding all places saved");

            var placeDocuments = await _context.Places.Find(_ => true).ToListAsync();

            return placeDocuments.Select(PlaceMapper.MapPlaceDocumentToPlaceEntity).ToList();
        }

    }
}
