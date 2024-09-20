using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.DTOs.Departments
{
    public class DepartmentDetailsDto
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public int LastModidiedBy { get; set; }
        public DateTime LastModidiedOn { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; } 
        public string Code { get; set; } = null!;
        public DateTime CreationDate { get; set; }
    }
}
