﻿
namespace TeachersGuardAPI.App.DTOs
{
    public class UserDtoOut
    {
        public string Id { get; set; } = "";
        public required string EmailOrEmployeeNumber { get; set; }
        public required string Name { get; set; }
        public required string Surnames { get; set; }
        public required string FaceImage { get; set; }
        public long AttendaceNumber { get; set; }
    }
}
