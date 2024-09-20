using LinkDev.IKEA.BLL.DTOs.Departments;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public class DepartmentService:IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<DepartmentDto> GetAllDeparments()
        {
            var departments = _departmentRepository.GetAllASIQueryable().Select(department => new DepartmentDto

            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreationDate = department.CreationDate,
            }
            ).AsNoTracking().ToList();
            return departments;
        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.Get(id);
            if (department is { })
            {
                return new DepartmentDetailsDto()
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                    CreatedOn = department.CreatedOn,
                    CreatedBy = department.CreatedBy,
                    LastModidiedBy = department.LastModidiedBy,
                    LastModidiedOn = department.LastModidiedOn,
                };
            }
            return null;
        }



        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {

            var Department = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                // CreatedOn = DateTime.UtcNow,
                CreatedBy = 1,
                LastModidiedBy = 1,
                LastModidiedOn = DateTime.UtcNow,
            };

            return _departmentRepository.Add(Department);
        }

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.Get(id);
            if (department is { })
                return _departmentRepository.Delete(department) > 0;
            
            return false;
        }

        
       
        public int UpdateDepartment(DepartmentDto departmentDto)
        {

            var department = new Department()
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModidiedBy = 1,
                LastModidiedOn = DateTime.UtcNow,
            };

            return _departmentRepository.Update(department);
        }
    }
}
