using App.Domain.Core.HomeService.Contracts.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.HomeServices
{
    public class UploadService : IUploadService
    { 
        public async Task<string> AddFile(IFormFile file, string uploadPath)
        {
            var random = Guid.NewGuid() + file.FileName;
            using (var filestream = new FileStream(Path.Combine(uploadPath, random), FileMode.Create))
            {
                await file.CopyToAsync(filestream);
            }
            return random;

        }
    }
}
