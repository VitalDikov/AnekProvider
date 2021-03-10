using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnekProvider.DataModels.Repositories
{

    public interface IGenericRepository<TEntity>
            where TEntity : class
    {
        TEntity Create(TEntity item);
        TEntity FindById(Guid id);
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get(Func<TEntity, bool> predicate);
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}
