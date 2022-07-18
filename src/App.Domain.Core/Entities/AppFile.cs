using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Core.Entities
{
    public partial class AppFile
    {
        public AppFile()
        {
            OrderFiles = new HashSet<OrderFile>();
            ServiceFiles = new HashSet<ServiceFile>();
        }

        public int Id { get; set; }
        public int EntityId { get; set; }
        public string FileAddress { get; set; } = null!;

        [ForeignKey("AppUser")]
        public int CreatedUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Entity Entity { get; set; } = null!;
        public virtual ICollection<OrderFile> OrderFiles { get; set; }
        public virtual ICollection<ServiceFile> ServiceFiles { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
