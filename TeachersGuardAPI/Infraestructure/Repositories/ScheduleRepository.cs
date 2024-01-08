using MongoDB.Driver;
using TeachersGuardAPI.Domain.Entities;
using TeachersGuardAPI.Domain.Repositories;
using TeachersGuardAPI.Infraestructure.Mappers;
using TeachersGuardAPI.Infraestructure.Models.Mongo;
using TeachersGuardAPI.Infraestructure.Persistence.Context;

namespace TeachersGuardAPI.Infraestructure.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly MongoContext _context;
        private readonly ILogger<ScheduleRepository> _logger;
        public ScheduleRepository(MongoContext context, ILogger<ScheduleRepository> logger) 
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Schedule?> CreateSchedule(Schedule schedule)
        {
            _logger.LogInformation("Creating schedule");

            var scheduleToDb = ScheduleMapper.MapScheduleEntityToScheduleDocument(schedule);

            try
            {
                await _context.Schedules.InsertOneAsync(scheduleToDb);
                var scheduleId = scheduleToDb.Id.ToString();
                _logger.LogInformation("Schedyle created successfully with ID: " + schedule);

                schedule.ScheduleId = scheduleId;

                return schedule;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating schedule: {ex.Message}");
                return null;

            }
        }

        public async Task<List<Schedule>?> GetSchedulesByUserId(string userId)
        {
            _logger.LogInformation("Finding schedules for user with UserId: " + userId);

            var filter = Builders<ScheduleDocument>.Filter.Eq(u => u.UserId, userId);

            var scheduleDocuments = await _context.Schedules.Find(filter).ToListAsync();

            if (scheduleDocuments != null && scheduleDocuments.Any())
            {
                return scheduleDocuments.Select(ScheduleMapper.MapScheduleDocumentToScheduleEntity).ToList();
            }

            return null;
        }

        public async Task<bool> UserHasSchedule(string userId)
        {
            _logger.LogInformation("Checking if user with UserId: " + userId + " has schedules");

            var filter = Builders<ScheduleDocument>.Filter.Eq(u => u.UserId, userId);

            var scheduleCount = await _context.Schedules.CountDocumentsAsync(filter);

            return scheduleCount > 0;
        }


    }
}
