using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Model;

namespace WebAPI.Repository
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        public StoreRepository(FusionDBContext dbContext)
          : base(dbContext)
        {
        }
    }
}
