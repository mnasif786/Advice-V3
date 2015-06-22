using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;
using Peninsula.Domain.Entities;
using Peninsula.Domain.RepositoryContracts;

namespace Advice.Application.Implementations
{
    public class ClientService : IClientService
    {
        private readonly ITblCustomerRepository _tblCustomerRepository;
        private readonly ITBLSiteAddressRepository _tblSiteAddress;
        private readonly ICustomEntityRepository _customEntityRepository;
        public ClientService(ITblCustomerRepository tblCustomerRepository, ITBLSiteAddressRepository tblSiteAddress, ICustomEntityRepository customEntityRepository)
        {
            _tblCustomerRepository = tblCustomerRepository;
            _tblSiteAddress = tblSiteAddress;
            _customEntityRepository = customEntityRepository;
        }

        public IEnumerable<string> GetTop10CansStartWith(string can)
        {
            return _tblCustomerRepository.GetTop10CustomersStartWithCustomerKey(can).Select(cust => cust.CustomerKey);
        }

        public IEnumerable<string> GetTop10ClientNamesStartWith(string clientName)
        {
            return _tblCustomerRepository.GetTop10CustomersStartWithCompanyName(clientName).Select(cust => cust.CompanyName);
        }

        public ClientModel GetClientByName(string clientName)
        {
            var customer = _tblCustomerRepository.GetCustomerByCompanyName(clientName);
            //for gaining effeciency postcode is being sent as null as the call to this function does not need postcode to be returned
            // if postcode needs to be returned then see GetAllClientsStartWith to do the query to address
            return new ClientModel(customer.CustomerID, customer.CustomerKey, customer.CompanyName, string.Empty);
        }

        public ClientModel GetClientByCan(string can)
        {
            var customer = _tblCustomerRepository.GetCustomerByCustomerKey(can);
           
            if (customer != null)
            {
                //for gaining effeciency postcode is being sent as null as the call to this function does not need postcode to be returned
                // if postcode needs to be returned then see GetAllClientsStartWith to do the query to address
                return new ClientModel(customer.CustomerID, customer.CustomerKey, customer.CompanyName, string.Empty);    
            }

            return null;
        }
        public IEnumerable<ClientModel> GetTop10ClientsStartWithClientName(string clientName)
        {
            var customers = _tblCustomerRepository.GetTop10CustomersStartWithCompanyName(clientName);
            var tblCustomers = customers as IList<TBLCustomer> ?? customers.ToList();
            var customerIds = tblCustomers.Select(x => x.CustomerID).ToList();
            var addresses = _tblSiteAddress.GetSiteAddressByCustomerIds(customerIds).ToList();
            return tblCustomers.Select(cust => MapToClientModel(cust, addresses));

        }

        public IEnumerable<ClientModel> GetAllClientsStartWithClientName(string clientName)
        {
            var customers = _tblCustomerRepository.GetAllCustomersStartWithCompanyName(clientName);
            var tblCustomers = customers as IList<TBLCustomer> ?? customers.ToList();
            var customerIds = tblCustomers.Select(x => x.CustomerID).ToList();
            var addresses = _tblSiteAddress.GetSiteAddressByCustomerIds(customerIds).ToList();
            return tblCustomers.Select(cust => MapToClientModel(cust, addresses)).OrderBy(o => o.Can);
        }

        public IEnumerable<ClientModel> GetAllClientsStartWithCan(string can)
        {
            var customers = _tblCustomerRepository.GetAllCustomersStartWithCustomerKey(can);
            var tblCustomers = customers as IList<TBLCustomer> ?? customers.ToList();
            var customerIds = tblCustomers.Select(x => x.CustomerID).ToList();
            var addresses = _tblSiteAddress.GetSiteAddressByCustomerIds(customerIds).ToList();
            return tblCustomers.Select(cust => MapToClientModel(cust, addresses));
        }

        private ClientModel MapToClientModel(TBLCustomer customer, IEnumerable<TBLSiteAddress> addresses)
        {
            var address = addresses.SingleOrDefault(a => a.CustomerID == customer.CustomerID);
            return new ClientModel(customer.CustomerID, customer.CustomerKey, customer.CompanyName, address.Postcode);
        }

        public IEnumerable<ClientRecentActionModel> GetRecentClientsAction(string userName)
        {
            var customerActions = _customEntityRepository.GetRecentCustomersAction(userName);
            var recentCustomerActions = customerActions as IList<RecentCustomerAction> ?? customerActions.ToList();
            var customerIds = recentCustomerActions.Select(x => x.CustomerId).ToList();
            var customers = _tblCustomerRepository.GetCustomersByCustomerIds(customerIds).ToList();
            var clientRecentActions = recentCustomerActions.Select(action => MapToClientRecentActionModel(customers.First(c => c.CustomerID == action.CustomerId), action));
            return clientRecentActions.OrderByDescending(o=>o.LastAction);
        }

        public IEnumerable<string> GetCansStartWith(string can, short taskeRecords)
        {
            return _tblCustomerRepository.GetCustomersStartWithCustomerKey(can, taskeRecords).Select(cust => cust.CustomerKey);
        }

        private ClientRecentActionModel MapToClientRecentActionModel(TBLCustomer customer, RecentCustomerAction recentCustomerAction)
        {
            if (customer != null)
            {
                return new ClientRecentActionModel(customer.CustomerID,customer.CustomerKey, customer.CompanyName, recentCustomerAction.ActionDate);
            }

            return null;
        }
    }
}
