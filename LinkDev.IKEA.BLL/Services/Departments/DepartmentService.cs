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

        public IEnumerable<DepartmentDto> GetAllDeparments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAllASIQueryable().Where(D=>!D.IsDeleted).Select(department => new DepartmentDto

            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                CreationDate = department.CreationDate,
            }
            ).AsNoTracking().ToList();
            
            return departments;
        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.Get(id);
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

            _unitOfWork.DepartmentRepository.Add(Department);
            return _unitOfWork.Complete();
        }

        public bool DeleteDepartment(int id)
        {
            var DepartmentRepo=_unitOfWork.DepartmentRepository;
            var department = DepartmentRepo.Get(id);
            if (department is { })
                DepartmentRepo.Delete(department);
            
            return _unitOfWork.Complete()>0 ;
        }

        
       
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
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
            return _unitOfWork.Complete();
        }
    }
}
