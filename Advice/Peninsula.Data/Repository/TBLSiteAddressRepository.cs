using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peninsula.Data.Common;
using Peninsula.Data.Contracts;
using Peninsula.Domain.Entities;
using Peninsula.Domain.Entities.Enums;
using Peninsula.Domain.RepositoryContracts;

namespace Peninsula.Data.Repository
{
    public class TBLSiteAddressRepository : PeninsulaRepository<TBLSiteAddress>, ITBLSiteAddressRepository 
    {
        public TBLSiteAddressRepository(IPeninsulaDbContextManager dbContextManager)
            : base(dbContextManager)
        {

        }

        public IEnumerable<TBLSiteAddress> GetSiteAddressByCustomerIds(List<int> customerIds)
        {
            return Context.TBLSiteAddressSiteAddressTypes
                .Where(sat => sat.SiteAddressTypeID == (int)SiteAddressTypes.Main
                              && !sat.TBLSiteAddress.flagHidden.Value
                              && customerIds.Contains(sat.TBLSiteAddress.CustomerID.Value))
                .Include(s => s.TBLSiteAddress).Select(a => a.TBLSiteAddress).Distinct();
        }
    }
}
