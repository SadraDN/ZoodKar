using App.Domain.Core.BaseData.Entities;
using App.Domain.Core.User.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Core.HomeService.Entities
{
    public partial class ExpertFavoriteCategory
    {
        public int Id { get; set; }
        [ForeignKey("AppUser")]
        public int ExpertUserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual AppUser AppUser { get; set; } = null!;
    }
}
