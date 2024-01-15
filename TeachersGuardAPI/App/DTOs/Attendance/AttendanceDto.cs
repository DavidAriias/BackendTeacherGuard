namespace TeachersGuardAPI.App.DTOs.Attendance
{
    public class AttendanceDto
    {
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
        public required string UserId { get; set; }
        public bool FullAttendance { get; set; } = false;
        public required string PlaceId { get; set; }
        public required string PlaceName { get; set; }
        public required string AttendanceId { get; set; } 
    }
}
