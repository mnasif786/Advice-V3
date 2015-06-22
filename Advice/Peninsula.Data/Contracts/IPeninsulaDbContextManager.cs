using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peninsula.DataModel;

namespace Peninsula.Data.Contracts
{
    public interface IPeninsulaDbContextManager : IDisposable
    {
        PeninsulaEntities Context { get; }
        void BeginTransaction();
        void Rollback();
        void Save();
    }
}
