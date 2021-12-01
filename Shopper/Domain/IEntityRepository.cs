using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IEntityRepository<T> : IEntityRepository<T, int>
        where T : BaseEntity
    {

    }

    public interface IEntityRepository<T, TKey>
        where T : BaseEntity
        where TKey : struct
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(TKey id);
        Task Add(T entity);

    }
}
