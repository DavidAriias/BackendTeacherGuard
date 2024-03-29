﻿using TeachersGuardAPI.Domain.Entities;

namespace TeachersGuardAPI.Domain.Repositories
{
    public interface IScheduleRepository
    {
        public Task<List<Schedule>?> GetSchedulesByUserId(string userId);
        public Task<bool> UserHasSchedule(string emailOrEmployeeNumber);
        public Task<Schedule?> CreateSchedule(Schedule schedule);
        public Task<List<Schedule>?> GetSchedulesByPlaceId(string placeId);
    }
}
