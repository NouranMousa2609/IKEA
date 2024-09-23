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
        IEnumerable<EmployeeDto> GetAllEmployees();

        EmployeeDetailsDto? GetEmployeeById(int id);

        int CreateEmployee(CreatedEmployeeDto EmployeeDto);

        int UpdateEmployee(UpdatedEmployeeDto EmployeeDto);

        bool DeleteEmployee(int id);
    }
}
