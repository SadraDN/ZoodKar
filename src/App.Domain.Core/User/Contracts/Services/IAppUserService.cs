﻿using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.Services
{
    public interface IAppUserService
    {
        Task<AppUserDto>? Get(int id);
        Task<AppUserDto>? Get(string name);
        Task<int>? GetLoggedUserId();
        Task<List<AppUserDto>> GetAll(string? search);
        Task<IdentityResult> Create(AppUserDto dto);
        Task Update(AppUserDto dto);
        Task<SignInResult> Login(AppUserDto dto,bool rememberMe);
        Task SignOutUser();
        Task Delete(int id);
        Task<List<RolesDto>> GetRoles();
    }
}
