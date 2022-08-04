using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.Contracts.Services;
using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.User
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        public AppUserService(IAppUserRepository AppUserRepository)
        {
            _appUserRepository = AppUserRepository;
        }
        public async Task<IdentityResult> Create(AppUserDto dto)
        {
            return await _appUserRepository.Create(dto);
        }

        public async Task Delete(int id)
        {
            await _appUserRepository.Delete(id);
        }

        public async Task<AppUserDto>? Get(int id)
        {
            return await _appUserRepository.Get(id);
        }

        public Task<AppUserDto>? Get(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AppUserDto>> GetAll(string? search)
        {
            var users = await _appUserRepository.GetAll(search);
            return users;
        }

        public Task<int>? GetLoggedUserId()
        {
            return _appUserRepository.GetLoggedUserId();
        }

        public async Task<List<RolesDto>> GetRoles()
        {
            return await _appUserRepository.GetRoles();
        }

        public async Task<SignInResult> Login(AppUserDto dto, bool rememberMe)
        {
            return await _appUserRepository.Login(dto,rememberMe);
        }

        public async Task SignOutUser()
        {
            await _appUserRepository.SignOutUser();
        }

        public async Task Update(AppUserDto dto)
        {
            await _appUserRepository.Update(dto);
        }

        public async Task UpdateExpertSkills(int expertId, List<int> services, CancellationToken cancellationToken)
        {
            await _appUserRepository.UpdateExpertSkills(expertId, services, cancellationToken);
        }

        public async Task UpdateUsers(AppUserDto dto)
        {
            await _appUserRepository.UpdateUsers(dto);
        }
    }
}
