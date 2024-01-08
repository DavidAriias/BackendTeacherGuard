namespace TeachersGuardAPI.App.DTOs.Schedule
{
    public class ScheduleDtoIn
    {
        public required string PlaceId { get; set; }
        public required string UserId { get; set; }
        public required string Start { get; set; }
        public required string End { get; set; }
        public required List<DayOfWeek> DayOfWeek { get; set; }
    }
}
