using AnekProvider.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.DataModels.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private AnekContext _db;
        public IGenericRepository<VkUser> Users { get; private set; }
        public IGenericRepository<Anek> Aneks { get; private set; }

        public UnitOfWork(AnekContext db)
        {
            _db = db;
            Users = new GenericRepository<VkUser>(db, db.Users);
            Aneks = new GenericRepository<Anek>(db, db.Aneks);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public async void SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
