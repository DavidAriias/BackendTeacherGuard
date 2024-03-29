﻿using Microsoft.AspNetCore.Mvc;
using TeachersGuardAPI.App.DTOs.User;
using TeachersGuardAPI.App.UseCases.UserUseCase;

namespace TeachersGuardAPI.Presentation.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserUseCase _userUseCase;
        public UserController(UserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(CreateUserDto userDto)
        {
            var userId = await _userUseCase.CreateUserAsync(userDto);

            if (userId == null)
            {
               return StatusCode(500, new { Message = "Error al crear el usuario" });
            }

            return CreatedAtAction(nameof(CreateUser), new { id = userId});
        }

        [HttpGet("get")]
        public async Task<ActionResult<UserDtoOut?>> GetUser([FromQuery] UserDtoIn userDto)
        {
            var user = await _userUseCase.GetUserAsync(userDto);

            if (user != null)
            {
                return Ok(user);
            }

            return Unauthorized(new { Message = "Credenciales incorrectas, intente nuevamente" });
        }

        [HttpGet("get-by-user-email-or-employee-number")]
        public async Task<ActionResult<UserDtoOut?>> GetUserByEmailOrEmployeeNumber(string emailOrEmployeeNumber)
        {
            var user = await _userUseCase.GetUserByEmailOrEmployeeNumberAsync(emailOrEmployeeNumber);

            if (user != null)
            {
                return Ok(user);
            }

            return Unauthorized(new { Message = "No hay usuarios con ese email o numero de empleado" });
        }

    }
}
