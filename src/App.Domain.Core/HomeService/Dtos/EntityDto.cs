using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Dtos
{
    public class EntityDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
    }
}
