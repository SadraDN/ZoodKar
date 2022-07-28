
using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface IAppUserRepository
    {
        #region "Queries"

        Task<AppUserDto>? Get(int id);
        Task<AppUserDto>? Get(string name);
        Task<List<AppUserDto>> GetAll(string? search);
        Task<List<RolesDto>> GetRoles();
        Task<int>? GetLoggedUserId();

        #endregion


        #region "Commands"

        Task<IdentityResult> Create(AppUserDto dto);
        Task Update(AppUserDto dto);
        Task<SignInResult> Login(AppUserDto dto, bool rememberMe);
        Task SignOutUser();
        Task Delete(int id);

        #endregion
    }
}
