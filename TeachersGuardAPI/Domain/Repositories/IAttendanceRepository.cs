using TeachersGuardAPI.Domain.Entities;

namespace TeachersGuardAPI.Domain.Repositories
{
    public interface IAttendanceRepository
    {
        public Task<string?> CreateAttendanceAsync(Attendance attendance);
        public Task<bool> UpdateAttendanceAsync(Attendance attendance);
        public Task<List<Attendance>?> GetAllAttendancesByUserIdAsync(string userId);
        public Task<List<Attendance>?> GetAttendancesByPlaceIdAsync(string placeId); 
    }
}
