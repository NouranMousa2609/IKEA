using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositories._Generic
{
    public class GenericRepository<T> :IGenericRepository<T> where T: ModelBase
        
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task< IEnumerable<T> >GetAllAsync(bool withAsNoTracking = true)
        {
            if (withAsNoTracking)
                return await _dbContext.Set<T>().Where(X=>!X.IsDeleted).AsNoTracking().ToListAsync();

            return await _dbContext.Set<T>().Where(X => !X.IsDeleted).ToListAsync();
        }
        public async Task <T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
            //return _dbContext.Find<T>(id);

            ///var T = _dbContext.Ts.Local.FirstOrDefault(D=>D.Id==id);
            ///if (T is null)
            ///{
            ///    T = _dbContext.Ts.FirstOrDefault(D => D.Id == id);
            ///}
            ///return T;
            ///
        }
        public void Add(T entity)=> _dbContext.Set<T>().Add(entity);

         

        public void Update(T entity)=>   _dbContext.Set<T>().Update(entity);

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);

        }

        public IQueryable<T> GetAllASIQueryable()
        {
            return _dbContext.Set<T>();
        }

        public IEnumerable<T> GetAllAsIEnumerable()
        {
            return _dbContext.Set<T>().ToList();
        }
    }
}
