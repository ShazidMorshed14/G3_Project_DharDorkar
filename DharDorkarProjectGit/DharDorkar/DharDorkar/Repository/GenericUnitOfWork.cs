using DharDorkar.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DharDorkar.Repository
{
    public class GenericUnitOfWork:IDisposable
    {
        private dbDharDorkarEntities1 DbEntity = new dbDharDorkarEntities1();
        public IRepository<Tbl_EntityType> GetRepositoryInstance<Tbl_EntityType>() where Tbl_EntityType : class
        {
            return new GenericRepository<Tbl_EntityType>(DbEntity);
        }
        public void SaveChanges()
        {
            DbEntity.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                DbEntity.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
    }
}