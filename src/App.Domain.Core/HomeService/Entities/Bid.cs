
using App.Domain.Core.User.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Core.HomeService.Entities
{
    public partial class Bid
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("AppUser")]
        public int ExpertUserId { get; set; }
        public int SuggestedPrice { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual AppUser AppUser { get; set; } = null!;
    }
}
