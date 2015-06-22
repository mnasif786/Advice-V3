using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peninsula.Domain.Entities;

namespace Peninsula.Domain.RepositoryContracts
{
    public interface ITBLSiteAddressRepository
    {
       IEnumerable<TBLSiteAddress> GetSiteAddressByCustomerIds(List<int> customerIds);
    }
}
