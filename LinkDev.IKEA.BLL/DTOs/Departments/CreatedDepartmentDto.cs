﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.DTOs.Departments
{
    public class CreatedDepartmentDto
    {
        [Required(ErrorMessage ="Code is Required!!")]
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateTime CreationDate { get; set; }
    }
}
