using PB_EGS_Escalavel.Application.InputModels;
using PB_EGS_Escalavel.Application.Services.Interfaces;
using PB_EGS_Escalavel.Application.ViewModels;
using PB_EGS_Escalavel.Core.Auth;
using PB_EGS_Escalavel.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB_EGS_Escalavel.Application.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public AccountService(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<LoginUserViewModel> Login(LoginUserInputModel inputModel)
        {
            // Utilizar o mesmo algoritmo para criar o hash dessa senha
            var passwordHash = _authService.ComputeSha256Hash(inputModel.Password);

            // Buscar no meu banco de dados um User que tenha meu e-mail e minha senha em formato hash
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(inputModel.Email, passwordHash);

            // se não existir, errro no login
            if (user == null) return null;

            // Se existir, gera o token usando os dados do usuário
            var token = _authService.GenerateJwtToken(user.Email, user.Role);

            return new LoginUserViewModel(user.Email, token);
        }
    }
}
