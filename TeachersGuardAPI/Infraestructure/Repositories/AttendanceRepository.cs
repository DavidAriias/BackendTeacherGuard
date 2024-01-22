using MongoDB.Bson;
using MongoDB.Driver;
using TeachersGuardAPI.Domain.Entities;
using TeachersGuardAPI.Domain.Repositories;
using TeachersGuardAPI.Infraestructure.Mappers;
using TeachersGuardAPI.Infraestructure.Models.Mongo;
using TeachersGuardAPI.Infraestructure.Persistence.Context;

namespace TeachersGuardAPI.Infraestructure.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly MongoContext _context;
        private readonly ILogger<UserRepository> _logger;
        public AttendanceRepository(MongoContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<string?> CreateAttendanceAsync(Attendance attendance)
        {
            var attendanceToDb = AttendanceMapper.MapAttendanceEntityToAttendanceDocument(attendance);

            try
            {
                await _context.Attendances.InsertOneAsync(attendanceToDb);
                var attendanceId = attendanceToDb.Id.ToString();

                _logger.LogInformation("Attendance created successfully with ID: " + attendanceId);

                return attendanceId;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating attendance: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Attendance>?> GetAllAttendancesByUserIdAsync(string userId)
        {
            _logger.LogInformation($"Getting all attendances for user with Id : {userId}");    

            var filter = Builders<AttendanceDocument>.Filter.Eq(u => u.UserId, ObjectId.Parse(userId));

            var attendanceDocuments = await _context.Attendances.Find(filter).ToListAsync();

            if (attendanceDocuments != null && attendanceDocuments.Any())
            {
                return attendanceDocuments.Select(AttendanceMapper.MapAttendanceDocumentToAttendanceEntity).ToList();
            }

            return null;
        }

        public async Task<List<Attendance>?> GetAttendancesByPlaceIdAsync(string placeId)
        {
            _logger.LogInformation("Getting all attendances for place with id :" + placeId);

            var filter = Builders<AttendanceDocument>.Filter.Eq(u => u.PlaceId, ObjectId.Parse(placeId));

            var attendanceDocuments = await _context.Attendances.Find(filter).ToListAsync();

            if (attendanceDocuments != null && attendanceDocuments.Any())
            {
                return attendanceDocuments.Select(AttendanceMapper.MapAttendanceDocumentToAttendanceEntity).ToList();
            }

            return null;
        }

        public async Task<bool> UpdateAttendanceAsync(Attendance attendance)
        {
            _logger.LogInformation("Updating attendance for user with ID: " + attendance.UserId);
            try
            {
                Console.WriteLine(attendance.AttendanceId);

                // Realiza las operaciones de actualización necesarias con las asistencias en el rango de tiempo actual
                attendance.FullAttendance = true;
                attendance.ExitDate = DateTime.Now;

                var updatingAttendanceDb = AttendanceMapper.MapAttendanceEntityToAttendanceDocument(attendance);

                updatingAttendanceDb.Id = ObjectId.Parse(attendance.AttendanceId);

                var filter = Builders<AttendanceDocument>.Filter.Eq(attendance => attendance.Id, updatingAttendanceDb.Id);

                // Actualiza la asistencia en la base de datos
                var updateResult = await _context.Attendances.ReplaceOneAsync(filter, updatingAttendanceDb);

                // Verifica si la actualización fue exitosa
                if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating attendance: {ex.Message}");
                return false;
            }
        }

    }
}
