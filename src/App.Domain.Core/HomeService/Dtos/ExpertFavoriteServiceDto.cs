using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Dtos
{
    public class ExpertFavoriteServiceDto
    {
        public int Id { get; set; }
        public int ExpertUserId { get; set; }
        public string? ExpertName { get; set; }
        public int ServiceId { get; set; }
        public string? ServiceTitle { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
