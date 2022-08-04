﻿using App.Domain.Core.HomeService.Dtos;
using App.Domain.Core.HomeService.Entities;
using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.Dtos;
using App.Domain.Core.User.Entities;
using App.Infrastructures.Database.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Repository.Ef.User
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _appDbContext;
        public AppUserRepository(UserManager<AppUser> UserManager,
            SignInManager<AppUser> SignInManager
            , RoleManager<IdentityRole<int>> RoleManager
            , IHttpContextAccessor httpContextAccessor,
            AppDbContext appDbContext)
        {
            _userManager = UserManager;
            _signInManager = SignInManager;
            _roleManager = RoleManager;
            _httpContextAccessor = httpContextAccessor;
            _appDbContext = appDbContext;
        }

        public async Task<IdentityResult> Create(AppUserDto dto)
        {
            var user = new AppUser
            {
                Id = dto.Id,
                Name = dto.Name,
                UserName = dto.UserName,
                Email = dto.Email,

            };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (dto.Roles != null)
            {
                foreach (var role in dto.Roles)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "CustomerRole");
            }

            return result;
        }

        public async Task Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(user);
        }

        public async Task<AppUserDto>? Get(int id)
        {
            var user = await _appDbContext.Users.Where(x => x.Id == id).Select(x => new AppUserDto()
            {
                Id = x.Id,
                Name = x.Name,
                UserName = x.UserName,
                Email = x.Email,
                HomeAddress = x.HomeAddress,
                IsActive = x.IsActive,
                PictureFileId = x.PictureFileId,
                Services = x.ExpertFavoriteServices.Select(e => e.Service).Select(d => new ServiceDto()
                {
                    Id = d.Id,
                    CategoryId = d.CategoryId,
                    Price = d.Price,
                    ShortDescription = d.ShortDescription,
                    CategoryName = d.Category.Title,
                    Title = d.Title,

                }).ToList(),
                
            }).FirstOrDefaultAsync();
            var result = await _userManager.FindByIdAsync(id.ToString());
            var roles = await _userManager.GetRolesAsync(result);
            user.Roles = roles;
            return user;
        }

        public Task<AppUserDto>? Get(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AppUserDto>> GetAll(string? serach)
        {
            var users = await _userManager.Users.Select(p => new AppUserDto()
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                HomeAddress = p.HomeAddress,
                IsActive = p.IsActive,
                PictureFileId = p.PictureFileId,
                UserName = p.UserName,
                ExpertFavoriteServices = p.ExpertFavoriteServices.Select(x => new ExpertFavoriteServiceDto
                {
                    ServiceId = x.ServiceId,
                    ServiceTitle = x.Service.Title,
                    CreatedAt = x.CreatedAt,
                    ExpertName = x.AppUser.Name,
                    ExpertUserId = x.AppUser.Id,
                    Id = x.Id,
                }).ToList(),
                
            }).ToListAsync();
            foreach (var item in users)
            {
                var userRole = await _userManager.GetRolesAsync(await _userManager.Users.FirstAsync(x => x.Id == item.Id));
                item.Roles = userRole;
            }

            if (string.IsNullOrWhiteSpace(serach))
            {
                return users;
            }
            serach = serach.ToLower();
            users = users.Where(x => x.Email.ToLower() == serach || x.UserName.ToLower().Contains(serach)).ToList();
            return users;

        }

        public async Task<int>? GetLoggedUserId()
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var id = user.Id;
            return id;
        }

        public async Task<List<RolesDto>> GetRoles()
        {
            return await _roleManager.Roles.Select(x => new RolesDto()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
        }

        public async Task<SignInResult> Login(AppUserDto dto, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, rememberMe, false);
        }

        public async Task SignOutUser()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task Update(AppUserDto dto)
        {
            var user1 = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                await _userManager.RemovePasswordAsync(user1);
                await _userManager.AddPasswordAsync(user1, dto.Password);
            }

            user1.Email = dto.Email;
            user1.Name = dto.Name;
            user1.UserName = dto.UserName;
            var roles = await _userManager.GetRolesAsync(user1);
            await _userManager.RemoveFromRolesAsync(user1, roles);
            await _userManager.AddToRolesAsync(user1, dto.Roles);
            await _userManager.UpdateAsync(user1);
        }

        public async Task UpdateExpertSkills(int expertId, List<int> services, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.ExpertFavoriteCategories.Where(c => c.ExpertUserId == expertId).ToListAsync();
            foreach (var service in result)
            {
                _appDbContext.ExpertFavoriteCategories.Remove(service);
            }
            foreach (var service in services)
            {
                ExpertFavoriteService skills = new()
                {
                    ExpertUserId = expertId,
                    ServiceId = service,
                    CreatedAt = DateTime.Now,
                };
                await _appDbContext.ExpertFavoriteCategories.AddAsync(skills, cancellationToken);
            }
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateUsers(AppUserDto dto)
        {
            var user1 = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                await _userManager.RemovePasswordAsync(user1);
                await _userManager.AddPasswordAsync(user1, dto.Password);
            }

            user1.Email = dto.Email;
            user1.Name = dto.Name;
            user1.UserName = dto.UserName;
            await _userManager.UpdateAsync(user1);
        }
    }
}

