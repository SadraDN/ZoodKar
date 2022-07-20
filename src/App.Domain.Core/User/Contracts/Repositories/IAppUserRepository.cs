
using App.Domain.Core.User.Dtos;


namespace App.Domain.Core.User.Contracts.Repositories
{
    public interface IAppUserRepository
    {
        #region "Queries"

        Task<AppUserDto>? Get(int id);
        Task<AppUserDto>? Get(string fileAddress);
        Task<List<AppUserDto>> GetAll();

        #endregion


        #region "Commands"

        Task Add(AppUserDto dto, string password);
        Task Update(AppUserDto dto);
        Task Delete(int id);

        #endregion
    }
}
