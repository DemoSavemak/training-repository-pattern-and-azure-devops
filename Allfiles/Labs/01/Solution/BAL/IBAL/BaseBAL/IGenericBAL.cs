using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace WebAPI.BAL
{
    public interface IGenericBAL<TEntity> 
        where TEntity : class
    {
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByID(int? id);
        Task<TEntity> GetByExp(Expression<Func<TEntity, bool>> predicate);
        Task<bool> Update(TEntity entity);
        Task<bool> Create(TEntity entity);
        //Task<bool> Delete(int id);
        Task<bool> Delete(TEntity entity );
    }
}
