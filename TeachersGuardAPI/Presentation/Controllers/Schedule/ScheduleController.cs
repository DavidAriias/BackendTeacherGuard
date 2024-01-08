using Microsoft.AspNetCore.Mvc;
using TeachersGuardAPI.App.DTOs.Schedule;
using TeachersGuardAPI.App.UseCases.Schedule;

namespace TeachersGuardAPI.Presentation.Controllers.Schedule
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleUseCase _scheduleUseCase;
        public ScheduleController(ScheduleUseCase scheduleUseCase)
        {
            _scheduleUseCase = scheduleUseCase;
        }

        [HttpGet("get-list-schedule")]
        public async Task<ActionResult<List<ScheduleDtoOut>?>> GetScheduleByUserId(string userId)
        {
            if (userId == null) return BadRequest("userId must be provided");

            var schedules = await _scheduleUseCase.GetScheduleByUserId(userId);

            return schedules != null ? Ok(schedules) : NotFound("Este usuario no tiene un horario asignado");
        }

        [HttpPost("create")]
        public async Task<ActionResult<ScheduleDtoOut?>> CreateSchedule(ScheduleDtoIn schedule)
        {
           var scheduleResponse = await _scheduleUseCase.CreateSchedule(schedule);

            if (scheduleResponse == null) return StatusCode(500, "Error al crear el horario, intente mas tarde");

            return Ok(scheduleResponse);
        }

    }
}
