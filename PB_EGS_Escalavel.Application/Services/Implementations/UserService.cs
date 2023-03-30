using PB_EGS_Escalavel.Core.Entities;
using PB_EGS_Escalavel.Application.InputModels;
using PB_EGS_Escalavel.Application.Services.Interfaces;
using PB_EGS_Escalavel.Application.ViewModels;
using PB_EGS_Escalavel.Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PB_EGS_Escalavel.Core.Repositories;
using PB_EGS_Escalavel.Core.Auth;

namespace PB_EGS_Escalavel.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<int> CreateAsync(NewUserInputModel inputModel)
        {
            var passwordHash = _authService.ComputeSha256Hash(inputModel.Password);
            var user = new User(inputModel.FullName, inputModel.Email, inputModel.BirthDate, passwordHash, inputModel.Role);

            await _userRepository.AddAsync(user);

            return user.Id;
        }

        public async Task<UserViewModel> GetUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            return new UserViewModel(user.FullName, user.Email);
        }
    }
}
