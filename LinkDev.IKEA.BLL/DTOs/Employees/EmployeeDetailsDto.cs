﻿using LinkDev.IKEA.DAL.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.DTOs.Employees
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }


        [DataType(DataType.Currency)]

        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]

        public string? Address { get; set; }

        public bool IsActive { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public string Gender { get; set; } = null!;

        public string EmployeeType { get; set; } = null!;
        [Display(Name = "Phone Number")]
        [Phone]

        public string? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }


        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public int LastModidiedBy { get; set; }
        public DateTime LastModidiedOn { get; set; }


    }
}
