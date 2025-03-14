using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAPI.Model;
using System.Data.SqlClient;
using EFCore.BulkExtensions;

namespace WebAPI.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
     where TEntity : class
    {
        private readonly DBContext _dbContext;
        public GenericRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<TEntity> GetByExp(Expression<Func<TEntity, bool>> predicate )
        {
            try
            {
                return _dbContext.Set<TEntity>().Where(predicate).AsNoTracking();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public IQueryable<TEntity> GetByExp(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, string>> predicateorderby)
        {
            return _dbContext.Set<TEntity>().Where(predicate).OrderBy (predicateorderby).AsNoTracking();
        }
        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<IEnumerable< TEntity>> GetByExpAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }
        public async Task<bool> Create(TEntity entity)
        {
            try
            {
                await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                //                return false;
                throw;
            }
        }
        public async Task<bool> CreateRange( List<TEntity> entityList)
        {
            try
            {
               _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
                await _dbContext.Set<TEntity>().AddRangeAsync(entityList);
               _dbContext.ChangeTracker.DetectChanges();
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                //return false;
                throw;
            }
        }
        public async Task<bool> BulkInsertAsync (List<TEntity> entityList)
        {
            
            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    _dbContext.Database.SetCommandTimeout(3000);
                    await _dbContext.BulkInsertAsync<TEntity> (entityList, new BulkConfig { PreserveInsertOrder = false, SetOutputIdentity = false, BatchSize = 6000 , BulkCopyTimeout= 6000 });
                    transaction.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                //return false;
                throw;
            }
        }

        public bool BulkInsert(List<TEntity> entityList)
        {

            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    _dbContext.Database.SetCommandTimeout(3000);
                    _dbContext.BulkInsert<TEntity>(entityList, new BulkConfig { PreserveInsertOrder = false, SetOutputIdentity = false, BatchSize = 6000, BulkCopyTimeout = 3000 });

                    transaction.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update( TEntity entity)
        {
            try
            {
                _dbContext.Set<TEntity>().Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                //return false;
                throw;
            }
        }
        public async Task<bool> ExecuteSQL (string  SQLStatement)
        {
            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    _dbContext.Database.SetCommandTimeout(3000);
                    await _dbContext.Database.ExecuteSqlRawAsync(SQLStatement);
                    @transaction.Commit();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        public async Task<TEntity> GetByGuidIdAsync(Guid id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        public async Task<TEntity> GetByGuidId(Guid id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        public virtual async Task<ICollection<TEntity>> FindByAsyn(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetById(int? id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);

        }
        public int CountRow()
        {
            try
            {
                int RowCount = _dbContext.Set<TEntity>().Count();

                return RowCount;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        //public async Task <bool> Delete(int id)
        //{


        //    try
        //    {
        //        var entity = await GetById(id);
        //    _dbContext.Set<TEntity>().Remove(entity);
        //    await _dbContext.SaveChangesAsync();
        //        return true;

        //    catch
        //    {
        //        return false;

        //}

        //public async Task<bool> UpdateMultiple (Expression<Func<TEntity, bool>> predicate , Action<TEntity>  predicateforEach)
        //{
        //    try
        //    {
        //        await _dbContext.Set<TEntity>().Where(predicate).ForEachAsync(predicateforEach );
        //        await _dbContext.SaveChangesAsync();
        //        return true;

        //    catch (Exception ex)
        //    {
        //        return false;

        //}


        public async Task Update(Guid id, TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }


        //----




        //public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        //{
        //    IQueryable<TEntity> query = _dbContext.Set<TEntity>().Where(predicate);
        //    return query;
        //}

    }
}
