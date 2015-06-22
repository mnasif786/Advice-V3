using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;

namespace Advice.Data.Repository
{
    public class DivisionRepository : AdviceRepository<Division>, IDivisionRepository
    {
        public DivisionRepository(IAdviceDbContextManager adviceDbContextManager)
            : base(adviceDbContextManager)
        {
            
        }
    }
}
