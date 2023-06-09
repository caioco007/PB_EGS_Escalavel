﻿using PB_EGS_Escalavel.Application.InputModels;
using PB_EGS_Escalavel.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB_EGS_Escalavel.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginUserViewModel> Login(LoginUserInputModel inputModel);
    }
}
