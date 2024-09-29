using LinkDev.IKEA.BLL.DTOs.Departments;
using LinkDev.IKEA.BLL.DTOs.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public interface IDepartmentService
    {

        Task <IEnumerable<DepartmentDto>> GetAllDeparmentsAsync();

        Task <DepartmentDetailsDto?> GetDepartmentByIdAsync(int id);

        Task<int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto);

        Task <int> UpdateDepartmentAsync(UpdatedDepartmentDto departmentDto);

        Task <bool> DeleteDepartmentAsync(int id);
    }
}
