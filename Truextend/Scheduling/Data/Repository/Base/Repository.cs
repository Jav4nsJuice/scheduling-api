using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Truextend.Scheduling.Data.Models.Base;

namespace Truextend.Scheduling.Data.Repository.Base
{
	public class Repository<T> : IRepository<T> where T : Entity
	{
		public Repository()
		{
		}

        public Task<T> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

