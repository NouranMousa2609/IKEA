using Azure;
using LinkDev.IKEA.BLL.Common.Services.Attachments;
using LinkDev.IKEA.BLL.DTOs.Employees;
using LinkDev.IKEA.DAL.Common;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using LinkDev.IKEA.DAL.Persistance.UnitOfwork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        public EmployeeService(IUnitOfWork unitOfWork,IAttachmentService attachmentService)
        {
           _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
        }

        public async Task <int> CreateEmployeeAsync(CreatedEmployeeDto EmployeeDto)
        {
            var employee = new Employee()
            {
                Name = EmployeeDto.Name,
                Age = EmployeeDto.Age,
                Address = EmployeeDto.Address,
                Salary = EmployeeDto.Salary,
                IsActive = EmployeeDto.IsActive,
                Email = EmployeeDto.Email,
                PhoneNumber = EmployeeDto.PhoneNumber,
                HiringDate = EmployeeDto.HiringDate,
                Gender = EmployeeDto.Gender,
                EmployeeType = EmployeeDto.EmployeeType,
                CreatedBy = 1,
                LastModidiedBy = 1,
                LastModidiedOn = DateTime.UtcNow,
                DepartmentId = EmployeeDto.DepartmentId,


            };

            if (EmployeeDto.Image != null)
            {
                employee.Image =await _attachmentService.UploadAsync(EmployeeDto.Image, "Images");
            }
             _unitOfWork.EmployeeRepository.Add(employee);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task< bool> DeleteEmployeeAsync(int id)
        {
            var employeeRepo=_unitOfWork.EmployeeRepository;

            var employee = await employeeRepo.GetAsync(id);
            if (employee is { })
                 employeeRepo.Delete(employee);
            return await _unitOfWork.CompleteAsync()>0;
        }

        public async Task <IEnumerable<EmployeeDto>> GetEmployeesAsync(string search)
        {
            var employees = await _unitOfWork.EmployeeRepository.
                 GetAllASIQueryable()
                .Where(X => !X.IsDeleted && (string.IsNullOrEmpty(search)||X.Name.ToLower().Contains(search.ToLower())))
                .Include(E => E.Department)
                .Select(employee => new EmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                Department = employee.Department.Name?? "",


            }).ToListAsync();
            return employees;
        }

        public async Task <EmployeeDetailsDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id);
            if (employee is not null)
                return new EmployeeDetailsDto()
                {

                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    Department = employee.Department?.Name,
                    Image = employee.Image,


                };
            return null;
        }

        public async Task< int> UpdateEmployeeAsync(UpdatedEmployeeDto EmployeeDto)
        {
            var employee = new Employee()
            {
                Id = EmployeeDto.Id,
                Name = EmployeeDto.Name,
                Age = EmployeeDto.Age,
                Address = EmployeeDto.Address,
                Salary = EmployeeDto.Salary,
                IsActive = EmployeeDto.IsActive,
                Email = EmployeeDto.Email,
                PhoneNumber = EmployeeDto.PhoneNumber,
                HiringDate = EmployeeDto.HiringDate,
                Gender = EmployeeDto.Gender,
                EmployeeType = EmployeeDto.EmployeeType,
                CreatedBy = 1,
                LastModidiedBy = 1,
                LastModidiedOn = DateTime.UtcNow,
                DepartmentId = EmployeeDto.DepartmentId,

            };

             _unitOfWork.EmployeeRepository.Update(employee);
            return await _unitOfWork.CompleteAsync();
        }

        
    }
}
