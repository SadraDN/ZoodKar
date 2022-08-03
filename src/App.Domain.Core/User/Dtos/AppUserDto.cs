using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.User.Dtos
{
    public class AppUserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }
        public bool? IsActive { get; set; }
        public int? PictureFileId { get; set; }
        public string? PicUrl { get; set; }
        public string? HomeAddress { get; set; }
        public IList<string?> Roles { get; set; }
        public virtual List<ExpertFavoriteServiceDto> ExpertFavoriteCategories { get; set; } = new List<ExpertFavoriteServiceDto>();
    }
}
