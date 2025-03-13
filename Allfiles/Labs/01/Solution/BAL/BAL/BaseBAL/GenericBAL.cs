using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using WebAPI.Model;
using WebAPI.Repository;


namespace WebAPI.BAL
{
    public class GenericBAL<TEntity> : IGenericBAL<TEntity> 
        where TEntity : class
    {

        private readonly IGenericRepository<TEntity> _GenericRepository;
        public GenericBAL(IGenericRepository<TEntity> GenericRepository)
            
        {
            _GenericRepository = GenericRepository;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                 return _GenericRepository.GetByExp(predicate);  //.GetAll();
                                                                 //return _GenericRepository.GetAll();


                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public  async Task <TEntity> GetByExp(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return _GenericRepository.GetByExp(predicate).FirstOrDefault();//  .GetByExp (x => x.  Create(entity);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public Task<TEntity> GetByID(int? id)
        {
            try
            {
                return _GenericRepository.GetById(id);//  .GetByExp (x => x.  Create(entity);
                
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
        public async Task<bool> Create(TEntity entity)
        {
            try
            {
                return await _GenericRepository.Create(entity);

            }
            catch (Exception ex)
            {
                //return false;
                throw;
            }
        }
        public async Task<bool> Update(TEntity entity)
        {
            try
            {
               return await _GenericRepository.Update(entity);

            }
            catch (Exception ex)
            {
                return false;
                //throw;
            }
        }
        public async Task<bool> Delete (TEntity entity )
        {
            try
            {
                 return await _GenericRepository.Update(entity);

            }
            catch (Exception ex)
            {
                //return false;
                throw;
            }
        }



    }
}
