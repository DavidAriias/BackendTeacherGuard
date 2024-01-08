namespace TeachersGuardAPI.App.DTOs.Schedule
{
    public class ScheduleDtoOut
    {
        public required string PlaceId { get; set; }
        public required string ScheduleId { get; set; }
        public required string UserId { get; set; }
        public required string Start { get; set; }
        public required string End { get; set; }
        public required List<DayOfWeek> DayOfWeek { get; set; }
    }
}
