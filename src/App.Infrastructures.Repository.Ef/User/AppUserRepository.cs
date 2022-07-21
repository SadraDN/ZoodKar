using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.Dtos;
using App.Domain.Core.User.Entities;
using App.Infrastructures.Database.SqlServer;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Repository.Ef.User
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public AppUserRepository(AppDbContext context
            , UserManager<AppUser> UserManager)
        {
            _context = context;
            _userManager = UserManager;
        }

        public async Task Add(AppUserDto dto, string password)
        {
            AppUser record = new()
            {
                Name = dto.Name,
                PictureFileId = dto.PictureFileId,
                HomeAddress = dto.HomeAddress,
            };
            await _userManager.CreateAsync(record, password);
        }

        public Task Update(AppUserDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AppUserDto>? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AppUserDto>? Get(string fileAddress)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppUserDto>> GetAll()
        {
            throw new NotImplementedException();
        }


    }
}
