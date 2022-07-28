using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Contracts.Services;
using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.User
{
    public class AppUserAppService : IAppUserAppService
    {
        private readonly ILogger<AppUserAppService> _logger;
        private readonly IAppUserService _appUserService;
        public AppUserAppService(IAppUserService AppUserService
            , ILogger<AppUserAppService> logger)
        {
            _appUserService = AppUserService;
            _logger = logger;
        }
        public async Task<IdentityResult> Create(AppUserDto dto)
        {
            return await _appUserService.Create(dto);
        }

        public async Task Delete(int id)
        {
            await _appUserService.Delete(id);
        }

        public async Task<AppUserDto>? Get(int id)
        {
            return await _appUserService.Get(id);
        }

        public Task<AppUserDto>? Get(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AppUserDto>> GetAll(string? search)
        {
            return await _appUserService.GetAll(search);
        }

        public Task<int>? GetLoggedUserId()
        {
            return _appUserService.GetLoggedUserId();
        }

        public async Task<List<RolesDto>> GetRoles()
        {
            return await _appUserService.GetRoles();
        }

        public async Task<SignInResult> Login(AppUserDto dto, bool rememberMe)
        {
            return await _appUserService.Login(dto,rememberMe);
        }

        public async Task SignOutUser()
        {
            await _appUserService.SignOutUser();
        }

        public async Task Update(AppUserDto dto)
        {
            await _appUserService.Update(dto);

        }
    }
}
