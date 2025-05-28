using LinkDev.IKEA.BLL.DTOs.Departments;
using LinkDev.IKEA.BLL.DTOs.Employees;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using LinkDev.IKEA.DAL.Persistance.UnitOfwork;
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
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task< IEnumerable<DepartmentDto>> GetAllDeparmentsAsync()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllASIQueryable().Where(D=>!D.IsDeleted).Select(department => new DepartmentDto

            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                CreationDate = department.CreationDate,
            }
            ).AsNoTracking().ToListAsync();
            
            return departments;
        }

        public async Task < DepartmentDetailsDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);
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



        public async Task <int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto)
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

             _unitOfWork.DepartmentRepository.Add(Department);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task< bool> DeleteDepartmentAsync(int id)
        {
            var DepartmentRepo=_unitOfWork.DepartmentRepository;
            var department = await DepartmentRepo.GetAsync(id);
            if (department is { })
                DepartmentRepo.Delete(department);
            
            return await _unitOfWork.CompleteAsync()>0 ;
        }

        
       
        public async Task <int> UpdateDepartmentAsync(UpdatedDepartmentDto departmentDto)
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

             _unitOfWork.DepartmentRepository.Update(department);
            return await _unitOfWork.CompleteAsync();
        }
    }
}
