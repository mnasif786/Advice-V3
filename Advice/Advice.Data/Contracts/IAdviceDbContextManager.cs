using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Advice.Domain;

namespace Advice.Data.Contracts
{
    public interface IAdviceDbContextManager:  IDisposable
    {
        AdviceEntities Context { get; }
        DbContextTransaction BeginTransaction();
        void Rollback();
        void Save();
    }
}
