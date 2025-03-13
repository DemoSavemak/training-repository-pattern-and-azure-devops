using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Model;
using WebAPI.Repository;
namespace WebAPI.BAL
{
   public class StoreBAL : GenericBAL<Store>, IStoreBAL
    {
        private readonly IGenericRepository<Store> _StoreRepository;
        public StoreBAL(IGenericRepository<Store> StoreRepository)
            : base(StoreRepository)
        {
            _StoreRepository = StoreRepository;
        }


    }
}
