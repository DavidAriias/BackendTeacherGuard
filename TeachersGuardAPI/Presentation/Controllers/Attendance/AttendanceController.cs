﻿using Microsoft.AspNetCore.Mvc;
using TeachersGuardAPI.App.DTOs.Attendance;
using TeachersGuardAPI.App.UseCases.Attendace;
using TeachersGuardAPI.App.UseCases.Schedule;
using TeachersGuardAPI.App.UseCases.UserUseCase;

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
        public async Task<IActionResult> RegisterEntryAttendance(string emailOrEmployeeNumber)
        {
           
            var user = await _userUseCase.GetUserByEmailOrEmployeeNumberAsync(emailOrEmployeeNumber);

            if (user == null) return BadRequest(new { Message = "Usuario no encontrado" });


            var userHasSchedule = await _scheduleUseCase.UserHasSchedule(user.Id);

            if (!userHasSchedule) return BadRequest(new { Message = "Este usuario no tiene algun horario registrado" });


            var attendanceResult = await _attendanceUseCase.RegisterEntryAttendance(user.Id);

            if (attendanceResult != null) return Conflict(new { Message = attendanceResult });

            return Ok(new { Message = "La entrada ha sido generada exitosamente"});
        }
        
        [HttpPost("register-exit")]
        public async Task<IActionResult> RegisterExitAttendance(string emailOrEmployeeNumber)
        {


            var user = await _userUseCase.GetUserByEmailOrEmployeeNumberAsync(emailOrEmployeeNumber);

            if (user == null) return BadRequest(new { Message = "Usuario no encontrado" });

            var attendanceResult = await _attendanceUseCase.RegisterExitAttendanceByUserId(user.Id);

            if (attendanceResult == null) return Ok(new { Message = "El registro de salida fue guardado exitosamente" });

            return Conflict(new { Message = attendanceResult });
        }

        [HttpGet("get-list-attendances")]
        public async Task<ActionResult<List<AttendanceDto>>?> GetAttendancesByUserId(string userId)
        {
            var isUserExists = await _userUseCase.FindUserById(userId);

            if (!isUserExists) return BadRequest(new { Message = "Usuario no encontrado" });

            var attendances = await _attendanceUseCase.GetListAttendancesByUserId(userId);

            if (attendances == null) return Conflict(new { Message = "Usuario no tiene registrada assitencias" });

            return Ok(new { Attendances = attendances });

        }

        [HttpGet("get-week-attendances")]
        public async Task<ActionResult<WeekAttendanceDto>> GetWeekAttendancesByUserId(string userId)
        {
            var weekAttendance = await _attendanceUseCase.GetWeekAttendanceByUserIdAsync(userId);

            if (weekAttendance == null) return NotFound(new { Message = "Usuario no encontrado"});

            return Ok(new { WeekAttendances = weekAttendance });
        }
    }
}
