using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Contracts.AppServices
{
    public interface IAppUserAppService
    {
        Task<AppUserDto>? Get(int id);
        Task<AppUserDto>? Get(string name);
        Task<List<AppUserDto>> GetAll(string? search, CancellationToken cancellationToken);
        Task<int>? GetLoggedUserId();
        Task UpdateUsers(AppUserDto dto);
        Task UpdateExpertSkills(int expertId, List<int> services, CancellationToken cancellationToken);
        Task<IdentityResult> Create(AppUserDto dto);
        Task Update(AppUserDto dto);
        Task<SignInResult> Login(AppUserDto dto,bool rememberMe);
        Task SignOutUser();
        Task Delete(int id);
        Task<List<RolesDto>> GetRoles();
    }
}
