using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Peninsula.Domain.Common
{
    public interface IPeninsulaRepository<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(object id);
        IEnumerable<TEntity> Search(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        void Insert(TEntity entity);

        void Update(TEntity entityToUpdate);

        void SaveChanges();
    }
}
