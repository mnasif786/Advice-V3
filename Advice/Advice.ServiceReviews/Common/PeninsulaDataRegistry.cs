using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.Repository;
using Advice.Data.Repository.Services;
using Advice.Domain.RepositoryContracts;
using Advice.Domain.RepositoryContracts.Services;
using Peninsula.Data.Common;
using Peninsula.Data.Contracts;
using Peninsula.Data.Repository;
using Peninsula.Domain.RepositoryContracts;
using StructureMap.Configuration.DSL;

namespace Advice.ServiceReviews.Common
{
    public class PeninsulaDataRegistry : Registry
    {

        public PeninsulaDataRegistry()
        {
            For<IPeninsulaDbContextManager>().Use<PeninsulaDbContextManager>();
            For<ITblCustomerRepository>().Use<TblCustomerRepository>();
        }
    }
}
