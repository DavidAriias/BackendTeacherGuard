using TeachersGuardAPI.App.DTOs.Schedule;
using TeachersGuardAPI.Domain.Repositories;
using TeachersGuardAPI.Infraestructure.Mappers;

namespace TeachersGuardAPI.App.UseCases.Schedule
{
    public class ScheduleUseCase
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IPlaceRepository _placeRepository;
        public ScheduleUseCase(IScheduleRepository scheduleRepository , IPlaceRepository placeRepository) 
        {
            _scheduleRepository = scheduleRepository;
            _placeRepository = placeRepository;
        }

        public async Task<List<ScheduleDtoOut>?> GetScheduleByUserId(string userId)
        {
            var schedules = await _scheduleRepository.GetSchedulesByUserId(userId);

            if (schedules == null) return null;

            var scheduleDtos = schedules.Select(ScheduleMapper.MapScheduleEntityToScheduleDto).ToList();

            foreach (var scheduleDto in scheduleDtos)
            {
               var place  = await _placeRepository.GetPlaceByPlaceId(scheduleDto.PlaceId);
               scheduleDto.PlaceName = place?.Name ?? "";
            }

            return scheduleDtos;
        }


        public async Task<bool> UserHasSchedule(string userId)
        {
            return await _scheduleRepository.UserHasSchedule(userId);
        }

        public async Task<ScheduleDtoOut?> CreateSchedule(ScheduleDtoIn schedule)
        {
            var scheduleEntity = ScheduleMapper.MapScheduleDtoInToScheduleEntity(schedule);

            var scheduleResponse = await _scheduleRepository.CreateSchedule(scheduleEntity);

            if (scheduleResponse == null) return null;

            return ScheduleMapper.MapScheduleEntityToScheduleDto(scheduleResponse);
        }
    }
}
