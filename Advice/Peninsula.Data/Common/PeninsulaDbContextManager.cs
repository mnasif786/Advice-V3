using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peninsula.Data.Contracts;
using Peninsula.DataModel;

namespace Peninsula.Data.Common
{
    public class PeninsulaDbContextManager : IPeninsulaDbContextManager
    {
        private bool _disposed;

        private readonly PeninsulaEntities _context;

        public PeninsulaEntities Context { get { return _context; } }

        public PeninsulaDbContextManager(PeninsulaEntities context)
        {
            _context = context;
        }
        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
