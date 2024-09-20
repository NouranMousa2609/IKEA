﻿using LinkDev.IKEA.DAL.Models.Departments;
using LinkDev.IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositories.Departments
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Department> GetAll(bool withAsNoTracking = true)
        {
            if (withAsNoTracking)
             return _dbContext.Departments.AsNoTracking().ToList();

            return _dbContext.Departments.ToList();
        }
        public Department? GetById(int id)
        {
            return _dbContext.Departments.Find(id);
            //return _dbContext.Find<Department>(id);

            ///var department = _dbContext.Departments.Local.FirstOrDefault(D=>D.Id==id);
            ///if (department is null)
            ///{
            ///    department = _dbContext.Departments.FirstOrDefault(D => D.Id == id);
            ///}
            ///return department;
            ///
        }
        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);

            return _dbContext.SaveChanges();
        }

        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);

            return _dbContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);

            return _dbContext.SaveChanges();
        }
    }
}
