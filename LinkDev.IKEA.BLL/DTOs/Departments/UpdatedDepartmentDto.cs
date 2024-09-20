﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.DTOs.Departments
{
    internal class UpdatedDepartmentDto
    {
        public int Id { get; set; }

        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string? Description { get; set; } 

        public DateTime CreationDate { get; set; }
    }
}
