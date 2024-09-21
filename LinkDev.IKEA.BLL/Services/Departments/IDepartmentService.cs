using LinkDev.IKEA.BLL.DTOs.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public interface IDepartmentService
    {

        IEnumerable<DepartmentDto> GetAllDeparments();

        DepartmentDetailsDto? GetDepartmentById(int id);

        int CreateDepartment(CreatedDepartmentDto departmentDto);

        int UpdateDepartment(UpdatedDepartmentDto departmentDto);

        bool DeleteDepartment(int id);
    }
}
