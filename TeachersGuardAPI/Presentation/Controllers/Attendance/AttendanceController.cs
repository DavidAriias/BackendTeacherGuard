using Microsoft.AspNetCore.Mvc;
using TeachersGuardAPI.App.DTOs.Attendance;
using TeachersGuardAPI.App.UseCases.Attendace;
using TeachersGuardAPI.App.UseCases.Schedule;
using TeachersGuardAPI.App.UseCases.User;

namespace TeachersGuardAPI.Presentation.Controllers.Attendace
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly UserUseCase _userUseCase;
        private readonly AttendanceUseCase _attendanceUseCase;
        private readonly ScheduleUseCase _scheduleUseCase;
        public AttendanceController(
            UserUseCase userUseCase,
            AttendanceUseCase attendanceUseCase,
            ScheduleUseCase scheduleUseCase
            ) 
        {
            _userUseCase = userUseCase;
            _attendanceUseCase = attendanceUseCase;
            _scheduleUseCase = scheduleUseCase;
        }

        
        [HttpPost("register-entry")]
        public async Task<IActionResult> RegisterEntryAttendance(string userId)
        {
        
            var isUserExists = await _userUseCase.FindUserById(userId);

            if (!isUserExists) return BadRequest(new { Message = "Usuario no encontrado" });

          
            var userHasSchedule = await _scheduleUseCase.UserHasSchedule(userId);

            if (!userHasSchedule) return BadRequest(new { Message = "Este usuario no tiene algun horario registrado" });


            var attendanceResult = await _attendanceUseCase.RegisterEntryAttendance(userId);

            if (attendanceResult != null) return Conflict(new { Message = attendanceResult });

            return Ok(new { Message = "La entrada ha sido generada exitosamente"});
        }
        
        [HttpPost("register-exit")]
        public async Task<IActionResult> RegisterExitAttendance(string userId)
        {
            if (userId == null)
            {
                return BadRequest(new { Message = "userId is missing" });
            }

            var isUserExists = await _userUseCase.FindUserById(userId);

            if (!isUserExists) return BadRequest(new { Message = "Usuario no encontrado" });

            var attendanceResult = await _attendanceUseCase.RegisterExitAttendanceByUserId(userId);

            if (attendanceResult == null) return Ok(new { Message = "El registro de salida fue guardado exitosamente" });

            return Conflict(new { Message = attendanceResult });
        }

        [HttpGet("get-list-attendances")]
        public async Task<ActionResult<List<AttendanceDto>>?> GetAttendancesByUserId(string userId)
        {
            var isUserExists = await _userUseCase.FindUserById(userId);

            if (!isUserExists) return BadRequest(new { Message = "Usuario no encontrado" });

            var attendances = await _attendanceUseCase.GetListAttendancesByUserId(userId);

            return Ok(new { Attendances = attendances });

        }
    }
}
