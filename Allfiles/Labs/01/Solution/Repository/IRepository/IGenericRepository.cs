using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetByExp(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetByExp(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity,string >> predicateOrder);
        IQueryable<TEntity> GetAll();
        //IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable< TEntity>> GetByExpAsync(Expression<Func<TEntity, bool>> predicate);


        Task<bool> Create(TEntity entity);
        Task<bool> CreateRange(List<TEntity> entityList);
        Task<bool> BulkInsertAsync(List<TEntity> entityList);
        bool BulkInsert(List<TEntity> entityList);

        Task<bool> ExecuteSQL(string SQLStatement);
        int CountRow();

        Task<bool> Update(TEntity entity);
        //Task<bool> Delete(int id);
        


        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByGuidId(Guid id);
        Task<TEntity> GetById(int? id);
        Task Update(Guid id, TEntity entity);
        Task<TEntity> GetByGuidIdAsync(Guid id);
        Task<ICollection<TEntity>> FindByAsyn(Expression<Func<TEntity, bool>> predicate);

    }
}
