
using App.Domain.Core.User.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Core.HomeService.Entities
{
    public partial class ExpertFavoriteService
    {
        public int Id { get; set; }
        [ForeignKey("AppUser")]
        public int ExpertUserId { get; set; }
        public int ServiceId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Service Service { get; set; } = null!;
        public virtual AppUser AppUser { get; set; } = null!;
    }
}
