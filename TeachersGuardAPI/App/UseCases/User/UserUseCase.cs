using System.Net;
using TeachersGuardAPI.App.DTOs.User;
using TeachersGuardAPI.Config.helpers;
using TeachersGuardAPI.Domain.Repositories;
using TeachersGuardAPI.Infraestructure.Mappers;


namespace TeachersGuardAPI.App.UseCases.User
{
    public class UserUseCase
    {
        private readonly IUserRepository _userRepository;
        public UserUseCase(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<string?> CreateUserAsync(CreateUserDto user)
        {
            user.Password = EncryptHelper.GetPassEncrypt(user.Password);

           var userRepository = UserMapper.MapCreateUserDtoToUserEntity(user);
           return await _userRepository.CreateUserAsync(userRepository);

        }
        public async Task<UserDtoOut?> GetUserAsync(UserDtoIn user)
        {
            var userRepository = UserMapper.MapUserDTOInToUserEntity(user);
            var userFounded = await _userRepository.GetUserAsync(userRepository);

            if (userFounded is null || !EncryptHelper.VerifyPassword(user.Password, userFounded.Password))
            {
                return null;
            }

            return UserMapper.MapUserEntityToUserDtoOut(userFounded);
        }

        public async Task<bool> FindUserById(string userId)
        {
            return await _userRepository.IsExistUserById(userId);
        }
    }
}
