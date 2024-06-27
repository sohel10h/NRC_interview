using dotnet_registration_api.Data.Entities;
using dotnet_registration_api.Data.Models;
using dotnet_registration_api.Data.Repositories;
using dotnet_registration_api.Helpers;
using Mapster;

namespace dotnet_registration_api.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAllUsers();
        }
        public async Task<User> GetById(int id)
        {
          return  await _userRepository.GetUserById(id);
        }
        public async Task<User> Login(LoginRequest login)
        {
            throw new NotImplementedException();
        }
        public async Task<User> Register(RegisterRequest register)
        {
          var user = await  _userRepository.CreateUser(
             new User
             {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Username = register.Username,
                PasswordHash = HashHelper.HashPassword(register.Password)
            });
            return user;
        }
        public async Task<User> Update(int id, UpdateRequest updateRequest)
        {
            var user = await _userRepository.GetUserByUsernameAndPassword(updateRequest.Username, HashHelper.HashPassword(updateRequest.OldPassword));
            if (user == null) 
            {
                throw new NotFoundException();
            }
            return await _userRepository.UpdateUser(new User
            {
                FirstName = updateRequest.FirstName,
                LastName = updateRequest.LastName,
                Username = updateRequest.Username,
                PasswordHash = HashHelper.HashPassword(updateRequest.NewPassword)
            }); 
        }
        public async Task Delete(int id)
        {

           await _userRepository.DeleteUser(id);
        }

    }
}
