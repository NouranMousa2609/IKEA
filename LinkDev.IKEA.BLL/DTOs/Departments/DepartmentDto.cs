﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.DTOs.Departments
{
    public class DepartmentDto
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;
        [Display(Name ="Date of Creation")]
        public DateTime CreationDate { get; set; }

    }
}
