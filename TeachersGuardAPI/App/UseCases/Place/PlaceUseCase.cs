using TeachersGuardAPI.App.DTOs.Place;
using TeachersGuardAPI.Domain.Repositories;
using TeachersGuardAPI.Infraestructure.Mappers;

namespace TeachersGuardAPI.App.UseCases.PlaceUseCase
{
    public class PlaceUseCase
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IScheduleRepository _scheduleRepository;
        public PlaceUseCase(
            IPlaceRepository placeRepository,
            IAttendanceRepository attendanceRepository,
            IScheduleRepository scheduleRepository
            ) 
        {
            _placeRepository = placeRepository;
            _attendanceRepository = attendanceRepository;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<PlaceDto?> GetPlaceByPlaceId(string placeId)
        {
            var place = await _placeRepository.GetPlaceByPlaceId(placeId);

            if (place == null) return null;

            return PlaceMapper.MapPlaceEntityToPlaceDto(place);
        }

        public async Task<List<PlaceDto>> GetPlaces()
        {
            var places = await _placeRepository.GetPlaces();

            return places.Select(PlaceMapper.MapPlaceEntityToPlaceDto).ToList();
        }

        public async Task<UsePlaceDto?> GetUseByPlaceId(string placeId)
        {
            var attendances = await _attendanceRepository.GetAttendancesByPlaceIdAsync(placeId);
            var schedules = await _scheduleRepository.GetSchedulesByPlaceId(placeId);

            if (attendances == null || schedules == null) return null;

            return new UsePlaceDto
            {
                PlaceId = placeId,
                TotalAttendances = schedules.Sum(schedule => schedule.DayOfWeek.Count()),
                Attendances = attendances.Count()
            };
        }

    }
}
