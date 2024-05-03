using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Truextend.Scheduling.Data.Models.Base;

namespace Truextend.Scheduling.Data.Repository.Base
{
	public interface IRepository<T> where T : Entity
	{
        Task<T> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(T entity);
    }
}

