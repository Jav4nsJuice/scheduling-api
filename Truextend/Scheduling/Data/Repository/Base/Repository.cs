using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Truextend.Scheduling.Data.Exceptions;
using Truextend.Scheduling.Data.Models.Base;

namespace Truextend.Scheduling.Data.Repository.Base
{
	public class Repository<T> : IRepository<T> where T : Entity
	{
        protected readonly SchedulingDBContext dbContext;

		public Repository(SchedulingDBContext dbContext)
		{
            this.dbContext = dbContext;
        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                entity.Id = Guid.Empty;
                dbContext.Set<T>().Add(entity);
                await dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                throw new DatabaseException("ERROR: " + e.InnerException.Message);
            }
        }

        public async Task<T> DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var asd = dbContext.Set<T>();
            List<T> values = await asd.ToListAsync();
            return values;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            T value = await dbContext.Set<T>().FindAsync(id);
            return value;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}

