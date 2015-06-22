using System;
using System.Collections.Generic;
using Advice.Application.Contracts;
using Advice.Application.Implementations;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using Peninsula.Domain.Entities;
using Peninsula.Domain.RepositoryContracts;

namespace Application.Tests.Common
{
    public abstract class BaseClientServiceTests
    {
        protected Mock<ITblCustomerRepository> TblCustomerRepositoryMock;
        protected Mock<ITBLSiteAddressRepository> TblSiteAddressMock;
        protected Mock<ICustomEntityRepository> CustomEntityRepositoryMock;
        protected IClientService ClientService;
        protected IEnumerable<TBLCustomer> Customers;
        protected IEnumerable<TBLSiteAddress> Addresses;

        [SetUp]
        protected void BaseSetUp()
        {
            TblCustomerRepositoryMock = new Mock<ITblCustomerRepository>();
            TblSiteAddressMock = new Mock<ITBLSiteAddressRepository>();
            CustomEntityRepositoryMock = new Mock<ICustomEntityRepository>();
            ClientService = GetTarget();

            Customers = new List<TBLCustomer>()
            {
                new TBLCustomer()
                {
                    CustomerID = 29252,
                    CustomerKey = "ZPEN001",
                    CompanyName = "Client Data Solutions Ltd"
                },

                new TBLCustomer()
                {
                    CustomerID = 62692,
                    CustomerKey = "ZPEN002",
                    CompanyName = "Client Payments Caledonia"
                },

                new TBLCustomer()
                {
                    CustomerID = 50756,
                    CustomerKey = "ZPEN03",
                    CompanyName = "Client Services"
                },

                new TBLCustomer()
                {
                    CustomerID = 91186,
                    CustomerKey = "ZPEN004",
                    CompanyName = "Clientlogic"
                }

            };

            Addresses = new List<TBLSiteAddress>()
            {
                
                new TBLSiteAddress()
                {
                    SiteAddressID = 1,
                    CustomerID = 29252,
                    Address1 = "8 Rose Street",
                    Postcode = "M4 4FB",
                    flagHidden = false
                },

                new TBLSiteAddress()
                {
                    SiteAddressID = 2,
                    CustomerID = 62692,
                    Address1 = "8 Fin Street",
                    Postcode = "M8 8FB",
                    flagHidden = false
                },

                new TBLSiteAddress()
                {
                    SiteAddressID = 3,
                    CustomerID = 50756,
                    Address1 = "8 Emb Street",
                    Postcode = "M9 9FB",
                    flagHidden = false
                },

                new TBLSiteAddress()
                {
                    SiteAddressID = 4,
                    CustomerID = 91186,
                    Address1 = "8 Field Street",
                    Postcode = "M1 1FB",
                    flagHidden = false
                }
            };
        }

        protected IClientService GetTarget()
        {
            return new ClientService(TblCustomerRepositoryMock.Object, TblSiteAddressMock.Object, CustomEntityRepositoryMock.Object);
        }
    }
}
