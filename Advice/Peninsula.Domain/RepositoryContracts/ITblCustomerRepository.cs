using System.Collections;
using System.Collections.Generic;
using Peninsula.Domain.Common;
using Peninsula.Domain.Entities;

namespace Peninsula.Domain.RepositoryContracts
{
    public interface ITblCustomerRepository : IPeninsulaRepository<TBLCustomer>
    {
        TBLCustomer GetCustomerByCustomerKey(string customerKey);
        TBLCustomer GetCustomerByCompanyName(string companyName);
        /// <summary>
        /// Gets top 10 customers start with
        /// </summary>
        /// <param name="customerKey">customerKey</param>
        /// <returns>Customers</returns>
        IEnumerable<TBLCustomer> GetTop10CustomersStartWithCustomerKey(string customerKey);
        /// <summary>
        /// Gets top 10 customers start with
        /// </summary>
        /// <param name="companyName">companyName</param>
        /// <returns>Customers</returns>
        IEnumerable<TBLCustomer> GetTop10CustomersStartWithCompanyName(string companyName);
        /// <summary>
        /// Get all customers start with
        /// </summary>
        /// <param name="companyName">companyName</param>
        /// <returns>Customers</returns>
        IEnumerable<TBLCustomer> GetAllCustomersStartWithCompanyName(string companyName);
        IEnumerable<TBLCustomer> GetAllCustomersStartWithCustomerKey(string customerKey);
        IEnumerable<TBLCustomer> GetCustomersByCustomerKeys(List<string> customerKeys);
        IEnumerable<TBLCustomer> GetCustomersByCustomerIds(List<int> customerIds);

        IEnumerable<TBLCustomer> GetCustomersStartWithCustomerKey(string customerKey, short takeRecords);
        TBLCompanyContact GetCompanyContactWithContactId(long contactId);
    } 
}
