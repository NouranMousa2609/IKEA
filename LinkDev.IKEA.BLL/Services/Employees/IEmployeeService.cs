using LinkDev.IKEA.BLL.DTOs.Departments;
using LinkDev.IKEA.BLL.DTOs.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        Task <IEnumerable<EmployeeDto>> GetEmployeesAsync(string search);

        Task <EmployeeDetailsDto?> GetEmployeeByIdAsync(int id);

        Task <int> CreateEmployeeAsync(CreatedEmployeeDto EmployeeDto);

        Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto EmployeeDto);

        Task<bool> DeleteEmployeeAsync(int id);
    }
}
