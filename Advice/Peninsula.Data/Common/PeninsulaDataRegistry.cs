using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peninsula.Data.Contracts;
using Peninsula.Data.Repository;
using Peninsula.Domain.Entities;
using Peninsula.Domain.RepositoryContracts;
using StructureMap.Configuration.DSL;

namespace Peninsula.Data.Common
{
    public class PeninsulaDataRegistry : Registry
    {
        public PeninsulaDataRegistry()
        {
            For<IPeninsulaDbContextManager>().Use<PeninsulaDbContextManager>();
            For<ITblCustomerRepository>().Use<TblCustomerRepository>();
            For<ITBLSiteAddressRepository>().Use<TBLSiteAddressRepository>();
        }
    }
}
