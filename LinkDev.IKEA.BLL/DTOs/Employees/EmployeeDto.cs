﻿using LinkDev.IKEA.DAL.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.DTOs.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public int? Age { get; set; }


        [DataType(DataType.Currency)]

        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]

        public bool IsActive { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public Gender Gender { get; set; }

        public EmpType EmployeeType { get; set; }

        public string? Department { get; set; }

        public string? Image { get; set; }


    }
}
