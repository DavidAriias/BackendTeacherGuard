namespace TeachersGuardAPI.App.DTOs.Place
{
    public class UsePlaceDto
    {
        public required string PlaceId { get; set; }
        public required int TotalAttendances { get; set; }
        public required int Attendances { get; set; }
    }
}
