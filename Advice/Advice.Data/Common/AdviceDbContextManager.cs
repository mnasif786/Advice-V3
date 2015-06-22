using System;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Advice.Data.Contracts;
using Advice.Domain;
using Advice.Domain.Common;

namespace Advice.Data.Common
{
    public class AdviceDbContextManager : IAdviceDbContextManager
    {
        private bool _disposed;

        private readonly AdviceEntities _context;

        public AdviceEntities Context { get { return _context; } }

        public AdviceDbContextManager(AdviceEntities context)
        {
            _context = context;
        }
        public DbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
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
