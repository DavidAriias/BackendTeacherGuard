using TeachersGuardAPI.App.DTOs.Schedule;
using TeachersGuardAPI.Domain.Entities;
using TeachersGuardAPI.Infraestructure.Models.Mongo;

namespace TeachersGuardAPI.Infraestructure.Mappers
{
    public class ScheduleMapper
    {
        public static Schedule MapScheduleDocumentToScheduleEntity(ScheduleDocument scheduleDocument) => new()
        {

            UserId = scheduleDocument.UserId,
            PlaceId = scheduleDocument.PlaceId,
            ScheduleId = scheduleDocument.Id.ToString(),
            End = scheduleDocument.EndTime,
            Start = scheduleDocument.StartTime,
            DayOfWeek = scheduleDocument.DayOfWeek,
        };

        public static ScheduleDtoOut MapScheduleEntityToScheduleDto(Schedule schedule) => new()
        {
            DayOfWeek = schedule.DayOfWeek,
            End = schedule.End,
            PlaceId = schedule.PlaceId,
            ScheduleId = schedule.ScheduleId,
            Start = schedule.Start,
            UserId = schedule.UserId
        };

        public static Schedule MapScheduleDtoInToScheduleEntity(ScheduleDtoIn scheduleDto) => new()
        {
            DayOfWeek = scheduleDto.DayOfWeek,
            End = scheduleDto.End,
            Start = scheduleDto.Start,
            PlaceId = scheduleDto.PlaceId,
            UserId = scheduleDto.UserId,

        };

        public static ScheduleDocument MapScheduleEntityToScheduleDocument(Schedule schedule) => new()
        {
            DayOfWeek = schedule.DayOfWeek,
            EndTime = schedule.End,
            PlaceId = schedule.PlaceId,
            StartTime = schedule.Start,
            UserId= schedule.UserId,
        };
    }
}
