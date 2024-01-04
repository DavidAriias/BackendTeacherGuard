﻿using TeachersGuardAPI.App.DTOs.Attendance;
using TeachersGuardAPI.Config.helpers;
using TeachersGuardAPI.Domain.Entities;
using TeachersGuardAPI.Domain.Repositories;
using TeachersGuardAPI.Infraestructure.Mappers;

namespace TeachersGuardAPI.App.UseCases.Attendace
{
    public class AttendanceUseCase
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IScheduleRepository _scheduleRepository;
        public AttendanceUseCase(
            IAttendanceRepository attendanceRepository,
            IScheduleRepository scheduleRepository
            ) 
        {
            _attendanceRepository = attendanceRepository;
            _scheduleRepository = scheduleRepository;
        }


        public async Task<string?> RegisterEntryAttendance(string userId)
        {
            var attendancesAlreadyExist = await GetAttendanceByUserIdAndInRangeTime(userId);

            if (attendancesAlreadyExist != null)
                return "Este usuario ya tiene registrado la entrada para esta hora";

            var schedules = await _scheduleRepository.GetSchedulesByUserId(userId);

            var scheduleInTimeRange = schedules?
            .Where(schedule => DateHelper.IsCurrentTimeInRange(DateTime.Now, schedule.Start, schedule.End) 
            && schedule.DayOfWeek.Contains(DateTime.Now.DayOfWeek))
            .FirstOrDefault();

            if (scheduleInTimeRange == null)
                return "Este usuario no tiene alguna actividad para esta hora o dia";

            var attendance = new Attendance 
            {
                UserId = userId,
                PlaceId = scheduleInTimeRange.PlaceId,
                EntryDate = DateTime.Now,
                  
            };

            var attendanceResponse = await _attendanceRepository.CreateAttendanceAsync(attendance);

            if (attendanceResponse == null) return "La entrada no ha podido ser guardada";

            return null;
        }

        public async Task<string?> RegisterExitAttendanceByUserId(string userId)
        {

            var attendanceInRangeTime = await GetAttendanceByUserIdAndInRangeTime(userId);

            if (attendanceInRangeTime == null) 
                return "No se puede generar el registro de salida, porque no le toca en esta hora o no registro su entrada";

            if (attendanceInRangeTime.FullAttendance) return "Este usuario ya tiene un registro de salida para esta hora";

            var result = await _attendanceRepository.UpdateAttendanceAsync(attendanceInRangeTime);

            if (!result) return "El registro de salida no pudo ser guardado";

            return null;
        }

        private async Task<Attendance?> GetAttendanceByUserIdAndInRangeTime(string userId)
        {
            var attendances = await _attendanceRepository.GetAllAttendancesByUserIdAsync(userId);

            var currentTime = DateTime.UtcNow;
            var oneHourLater = currentTime.AddHours(1);

            return attendances?
            .FirstOrDefault(attendance =>
            attendance.EntryDate <= currentTime && currentTime <= attendance.EntryDate.AddHours(1));
        } 

        public async Task<List<AttendanceDto>?> GetListAttendancesByUserId(string userId)
        {
           var attendances = await _attendanceRepository.GetAllAttendancesByUserIdAsync(userId);

            if (attendances == null) return null;

            return attendances.Select(AttendanceMapper.MapAttendanceEntityToAttendanceDto).ToList();
        }
    }
}
