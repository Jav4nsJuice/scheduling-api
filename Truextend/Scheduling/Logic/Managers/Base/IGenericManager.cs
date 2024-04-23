using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Truextend.Scheduling.Logic.Managers.Base
{
	public interface IGenericManager<T> where T : class
	{
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid id);

        Task<T> Create(T item);

        Task<T> Update(T item, Guid id);

        Task<bool> Delete(Guid itemId);
    }
}

