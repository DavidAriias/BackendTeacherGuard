using MongoDB.Bson;
using TeachersGuardAPI.App.DTOs.Attendance;
using TeachersGuardAPI.Domain.Entities;
using TeachersGuardAPI.Infraestructure.Models.Mongo;

namespace TeachersGuardAPI.Infraestructure.Mappers
{
    public class AttendanceMapper
    {
        public static AttendanceDocument MapAttendanceEntityToAttendanceDocument(Attendance attendance) => new()
        {
            EntryDate = attendance.EntryDate,
            ExitDate = attendance.ExitDate,
            PlaceId = ObjectId.Parse(attendance.PlaceId),
            UserId = ObjectId.Parse(attendance.UserId),
            IsCompletedAttendance = attendance.FullAttendance
        };

        public static AttendanceDto MapAttendanceEntityToAttendanceDto(Attendance attendance) => new()
        {
            PlaceId = attendance.PlaceId,
            UserId = attendance.UserId,
            AttendanceId = attendance.AttendanceId,
            EntryDate = attendance.EntryDate,
            ExitDate = attendance.ExitDate,
            FullAttendance = attendance.FullAttendance,
            PlaceName = ""
        };

        public static Attendance MapAttendanceDocumentToAttendanceEntity(AttendanceDocument attendanceDocument) => new()
        {
            AttendanceId = attendanceDocument.Id.ToString(),
            FullAttendance = attendanceDocument.IsCompletedAttendance,
            PlaceId = attendanceDocument.PlaceId.ToString(),
            UserId = attendanceDocument.UserId.ToString(),
            EntryDate = attendanceDocument.EntryDate,
            ExitDate = attendanceDocument.ExitDate
        };
    }
}
