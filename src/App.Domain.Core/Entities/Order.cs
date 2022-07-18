using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Core.Entities
{
    public partial class Order
    {
        public Order()
        {
            Bids = new HashSet<Bid>();
            OrderFiles = new HashSet<OrderFile>();
            ServiceComments = new HashSet<ServiceComment>();
        }

        public int Id { get; set; }
        public byte StatusId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceBasePrice { get; set; }
        [ForeignKey("AppUser")]
        public int? CustomerUserId { get; set; }
        public int? FinalExpertUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Service Service { get; set; } = null!;
        public virtual OrderStatus Status { get; set; } = null!;
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<OrderFile> OrderFiles { get; set; }
        public virtual ICollection<ServiceComment> ServiceComments { get; set; }
    }
}
