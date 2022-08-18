
using App.Domain.Core.User.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Core.HomeService.Entities
{
    public partial class Order
    {
        public Order()
        {
            Bids = new HashSet<Bid>();
            OrderFiles = new HashSet<OrderFile>();
        }

        public int Id { get; set; }
        public byte StatusId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceBasePrice { get; set; }
        public DateTime ServiceDate { get; set; }
        public string SerivceAddress  { get; set; }
        public int? CustomerUserId { get; set; }
        public int? FinalExpertUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Service Service { get; set; } = null!;
        public virtual OrderStatus Status { get; set; } = null!;
        public virtual AppUser Customer { get; set; }
        public virtual AppUser Expert { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<OrderFile> OrderFiles { get; set; }
        public virtual List<ServiceComment> ServiceComments { get; set; } 
    }
}
