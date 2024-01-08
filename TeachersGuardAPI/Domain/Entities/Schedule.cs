namespace TeachersGuardAPI.Domain.Entities
{
    public class Schedule
    {
        public required string UserId { get; set; }
        public required string PlaceId { get; set; }
        public string ScheduleId { get; set; } = "";
        public required string Start {  get; set; }
        public required string End { get; set; }
        public required List<DayOfWeek> DayOfWeek { get; set; }
    }
}
