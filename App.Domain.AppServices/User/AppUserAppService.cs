using App.Domain.Core.HomeService.Contracts.Services;
using App.Domain.Core.HomeService.Dtos;
using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Contracts.Services;
using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        private readonly IAppFileService _appFileService;
        private readonly IConfiguration _configuration;
        private readonly IUploadService _uploadService;

        public AppUserAppService(IAppUserService AppUserService
            , ILogger<AppUserAppService> logger,
            IAppFileService appFileService, IConfiguration configuration
            , IUploadService uploadService)
        {
            _appUserService = AppUserService;
            _logger = logger;
            _appFileService = appFileService;
            _configuration = configuration;
            _uploadService = uploadService;
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

        public async Task<List<AppUserDto>> GetAll(string? search, CancellationToken cancellationToken)
        {
            var users = await _appUserService.GetAll(search);
            return users;
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
            return await _appUserService.Login(dto, rememberMe);
        }

        public async Task SignOutUser()
        {
            await _appUserService.SignOutUser();
        }

        public async Task Update(AppUserDto dto, IFormFile? profile, CancellationToken cancellationToken)
        {
            //if (profile != null)
            //{
            //    var loggeduser = await _appUserService.GetLoggedUserId();
            //    var user = await _appUserService.Get(loggeduser);
            //    if (user.AppFiles != null)
            //    {


            //    }

            //    var uploadpath = _configuration.GetSection("FileAddress:UserPath").Value;

            //        var fileName = await _uploadService.AddFile(profile, uploadpath);
            //        var fileId = await _appFileService.Set(new AppFileDto
            //        {
            //            CreatedAt = DateTime.Now,
            //            CreatedUserId =loggeduser,
            //            FileAddress = fileName,
            //            EntityId = 1
            //        }, cancellationToken);
            //    foreach (var pic in user.AppFiles)
            //    {
            //        await _appFileService.Update(pic.Id, cancellationToken);
            //    }
            //    var oFile = await _appFileService.Get(fileId, cancellationToken);
            //        dto.AppFiles.Add(oFile);

            await _appUserService.Update(dto);
        }

        public async Task UpdateExpertSkills(int expertId, List<int> services, CancellationToken cancellationToken)
        {
            await _appUserService.UpdateExpertSkills(expertId, services, cancellationToken);
        }

        public async Task UpdateUsers(AppUserDto dto, IFormFile? profile, CancellationToken cancellationToken)
        {
            var loggeduser = await _appUserService.GetLoggedUserId();
            var user = await _appUserService.Get(loggeduser);
            if (profile != null)
            {
                var uploadpath = _configuration.GetSection("FileAddress:UserPath").Value;
                var uploadProfile = await _uploadService.AddFile(profile, uploadpath);

                if (user.PictureFileId != null)
                {
                    AppFileDto fileload = await _appFileService.Get((int)user.PictureFileId, cancellationToken);
                    var deleteFile = Path.Combine(uploadpath,user.PicUrl);
                    File.Delete(deleteFile);
                    await _appFileService.Delete(fileload.Id, cancellationToken);
                }
                var fileId = await _appFileService.Set(new AppFileDto
                {
                    CreatedAt = DateTime.Now,
                    CreatedUserId = await _appUserService.GetLoggedUserId(),
                    FileAddress = uploadProfile,
                    EntityId = 2
                }, cancellationToken);
                dto.PictureFileId = fileId;
            }
            await _appUserService.UpdateUsers(dto);
        }
    }
}
