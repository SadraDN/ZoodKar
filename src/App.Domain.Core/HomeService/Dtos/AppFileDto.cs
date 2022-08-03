﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Dtos
{
    public class AppFileDto
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string FileAddress { get; set; } 
        public int CreatedUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
