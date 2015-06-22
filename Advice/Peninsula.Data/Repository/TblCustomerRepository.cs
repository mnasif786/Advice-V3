using System;
using System.Collections.Generic;
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
    public class TblCustomerRepository : PeninsulaRepository<TBLCustomer>, ITblCustomerRepository
    {
        public TblCustomerRepository(IPeninsulaDbContextManager dbContextManager)
            : base(dbContextManager)
        {

        }

        public TBLCustomer GetCustomerByCustomerKey(string customerKey)
        {
            return Context.TBLCustomers.SingleOrDefault(cust => cust.CustomerKey == customerKey && !cust.flagHidden.Value);
        }

        public TBLCustomer GetCustomerByCompanyName(string companyName)
        {
            return Context.TBLCustomers.FirstOrDefault(cust => cust.CompanyName == companyName && !cust.flagHidden.Value);
        }

        /// <summary>
        /// Gets top 10 customers start with
        /// </summary>
        /// <param name="customerKey">customeryKey</param>
        /// <returns>Customers</returns>

        public IEnumerable<TBLCustomer> GetTop10CustomersStartWithCustomerKey(string customerKey)
        {
            var customers = GetCustomerStartWithCustomerKeyQuery(customerKey);

            return customers.Distinct().Take(10);
        }

        /// <summary>
        /// Gets top 10 customers start with
        /// </summary>
        /// <param name="companyName">companyName</param>
        /// <returns>Customers</returns>

        public IEnumerable<TBLCustomer> GetTop10CustomersStartWithCompanyName(string companyName)
        {
            var customers = GetCustomerStartWithCompanyNameQuery(companyName);

            return customers.Distinct().Take(10);
        }

        /// <summary>
        /// Get all customers start with
        /// </summary>
        /// <param name="companyName">companyName</param>
        /// <returns>Customers</returns>
        public IEnumerable<TBLCustomer> GetAllCustomersStartWithCompanyName(string companyName)
        {
            return GetCustomerStartWithCompanyNameQuery(companyName);
        }

        public IEnumerable<TBLCustomer> GetAllCustomersStartWithCustomerKey(string customerKey)
        {
            return GetCustomerStartWithCustomerKeyQuery(customerKey);
        }

        public override TBLCustomer GetById(object id)
        {
            return Context.TBLCustomers.SingleOrDefault(cust => cust.CustomerID == (int)id);
        }

        public IEnumerable<TBLCustomer> GetCustomersByCustomerKeys(List<string> customerKeys)
        {
            return Context.TBLCustomers.Where(cust => customerKeys.Contains(cust.CustomerKey)).ToList(); //ToList() forces not to query database again on second reference
        }

        public IEnumerable<TBLCustomer> GetCustomersByCustomerIds(List<int> customerIds)
        {
            return Context.TBLCustomers.Where(cust => customerIds.Contains(cust.CustomerID));
        }

        public IEnumerable<TBLCustomer> GetCustomersStartWithCustomerKey(string customerKey, short takeRecords)
        {
            var customers = GetCustomerStartWithCustomerKeyQuery(customerKey);

            return customers.Distinct().Take(takeRecords);
        }

        public TBLCompanyContact GetCompanyContactWithContactId(long contactId)
        {
            var companyContact = Context.TBLCompanyContacts.SingleOrDefault(c => c.CompanyContactID == contactId);
            return companyContact;
        }

        private IQueryable<TBLCustomer> GetCustomerStartWithCustomerKeyQuery(string customerKey)
        {
            var customers = from tblCustomer in Context.TBLCustomers.Where(c => !c.flagHidden.Value && c.CustomerID != 2)
                            join siteAddress in Context.TBLSiteAddresses.Where(s => !s.flagHidden.Value)
                            on tblCustomer.CustomerID equals siteAddress.CustomerID
                            join siteAddressType in Context.TBLSiteAddressSiteAddressTypes.Where(sat => sat.SiteAddressTypeID == (int)SiteAddressTypes.Main)
                            on siteAddress.SiteAddressID equals siteAddressType.SiteAddressID
                            where tblCustomer.CustomerKey.StartsWith(customerKey)
                            select tblCustomer;
            return customers;
        }

        private IQueryable<TBLCustomer> GetCustomerStartWithCompanyNameQuery(string companyName)
        {
            var customers = from tblCustomer in Context.TBLCustomers.Where(c => !c.flagHidden.Value && c.CustomerID != 2)
                            join siteAddress in Context.TBLSiteAddresses.Where(s => !s.flagHidden.Value)
                            on tblCustomer.CustomerID equals siteAddress.CustomerID
                            join siteAddressType in Context.TBLSiteAddressSiteAddressTypes.Where(sat => sat.SiteAddressTypeID == (int)SiteAddressTypes.Main)
                            on siteAddress.SiteAddressID equals siteAddressType.SiteAddressID
                            where tblCustomer.CompanyName.StartsWith(companyName)
                            select tblCustomer;
            return customers;
        }
    }
}
