using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using Peninsula.Data.Common;
using Peninsula.Data.Contracts;
using Peninsula.Data.Repository;
using Peninsula.Data.Tests.TestHelpers;
using Peninsula.DataModel;
using Peninsula.Domain.Entities;
using Peninsula.Domain.RepositoryContracts;

namespace Peninsula.Data.Tests.Common
{
    public abstract class BaseTblCustomerRepositoryTests
    {
        protected IPeninsulaDbContextManager _dbContextManager;
        protected Mock<PeninsulaEntities> _peninsulaEntities;
        protected ITblCustomerRepository TblCustomerRepository;

        [SetUp]
        public void BaseFixtureSetup()
        {
            _peninsulaEntities = new Mock<PeninsulaEntities>();
            _dbContextManager = new PeninsulaDbContextManager(_peninsulaEntities.Object);

            var customerData = new List<TBLCustomer>()
            {
                new TBLCustomer()
                {
                    CompanyName = "Peninsula Business Services",
                    CustomerID = 1,
                    CustomerKey = "ZPEN01",
                    flagHidden = false
                },

                new TBLCustomer()
                {
                    CompanyName = "Client Data Solutions Ltd",
                    CustomerID = 2,
                    CustomerKey = "ZPEN02",
                    flagHidden = false
                },

                new TBLCustomer()
                {
                    CompanyName = "Client Payments Caledonia",
                    CustomerID = 3,
                    CustomerKey = "ZPEN03",
                    flagHidden = false
                },

                new TBLCustomer()
                {
                    CompanyName = "Client Services",
                    CustomerID = 4,
                    CustomerKey = "ZPEN04",
                    flagHidden = false
                },

                new TBLCustomer()
                {
                    CompanyName = "Clientlogic",
                    CustomerID = 5,
                    CustomerKey = "ZPEN05",
                    flagHidden = false
                },

                 new TBLCustomer()
                {
                    CompanyName = "Client Solutions Ltd",
                    CustomerID = 6,
                    CustomerKey = "ZPEN06",
                    flagHidden = false
                },

                new TBLCustomer()
                {
                    CompanyName = "Client Caledonia",
                    CustomerID = 7,
                    CustomerKey = "ZPEN07",
                    flagHidden = false
                },

                new TBLCustomer()
                {
                    CompanyName = "Client Karta Services",
                    CustomerID = 8,
                    CustomerKey = "ZPEN08",
                    flagHidden = false
                },

                new TBLCustomer()
                {
                    CompanyName = "Client Logic",
                    CustomerID = 9,
                    CustomerKey = "ZPEN09",
                    flagHidden = false
                },

                new TBLCustomer()
                {
                    CompanyName = "Client Mobile Services",
                    CustomerID = 10,
                    CustomerKey = "ZPEN02222",
                    flagHidden = false
                },

                new TBLCustomer()
                {
                    CompanyName = "Client Implement Logic",
                    CustomerID = 11,
                    CustomerKey = "ZPEN010",
                    flagHidden = false
                },

                new TBLCustomer()
                {
                    CompanyName = "Client Carrying Service",
                    CustomerID = 12,
                    CustomerKey = "ZPEN011",
                    flagHidden = false
                },

                new TBLCustomer()
                {
                    CompanyName = "Client Paper Service",
                    CustomerID = 13,
                    CustomerKey = "CPEN111",
                    flagHidden = false
                },

                 new TBLCustomer()
                {
                    CompanyName = "Client Sweep Service",
                    CustomerID = 14,
                    CustomerKey = "CPEN0111",
                    flagHidden = false
                }
            };

            var addressTypes = new List<TBLSiteAddressSiteAddressType>()
            {
                new TBLSiteAddressSiteAddressType()
                {
                    SiteAddressID = 1,
                    SiteAddressTypeID = 1,
                },

                new TBLSiteAddressSiteAddressType()
                {
                    SiteAddressID = 2,
                    SiteAddressTypeID = 1,
                    //TBLSiteAddress = addresses
                },

                new TBLSiteAddressSiteAddressType()
                {
                    SiteAddressID = 3,
                    SiteAddressTypeID = 1,
                },

                new TBLSiteAddressSiteAddressType()
                {
                    SiteAddressID = 4,
                    SiteAddressTypeID = 1,
                },

                new TBLSiteAddressSiteAddressType()
                {
                    SiteAddressID = 5,
                    SiteAddressTypeID = 1,
                    //TBLSiteAddress = addresses
                },

                new TBLSiteAddressSiteAddressType()
                {
                    SiteAddressID = 6,
                    SiteAddressTypeID = 1,
                },
                
                new TBLSiteAddressSiteAddressType()
                {
                    SiteAddressID = 7,
                    SiteAddressTypeID = 1,
                }
            };

            var addresses = new List<TBLSiteAddress>()
            {
                new TBLSiteAddress()
                {
                    SiteAddressID = 7,
                    CustomerID = 1,
                    Address1 = "8 Peter Street",
                    Postcode = "M2 2FB",
                    TBLSiteAddressSiteAddressTypes = addressTypes,
                    flagHidden = false
                },

                new TBLSiteAddress()
                {
                    SiteAddressID = 1,
                    CustomerID = 3,
                    Address1 = "8 Rose Street",
                    Postcode = "M4 4FB",
                    TBLSiteAddressSiteAddressTypes = addressTypes,
                    flagHidden = false
                },

                new TBLSiteAddress()
                {
                    SiteAddressID = 2,
                    CustomerID = 4,
                    Address1 = "8 Fin Street",
                    Postcode = "M8 8FB",
                    TBLSiteAddressSiteAddressTypes = addressTypes,
                    flagHidden = false
                },

                new TBLSiteAddress()
                {
                    SiteAddressID = 3,
                    CustomerID = 5,
                    Address1 = "8 Emb Street",
                    Postcode = "M9 9FB",
                    TBLSiteAddressSiteAddressTypes = addressTypes,
                    flagHidden = false
                },

                new TBLSiteAddress()
                {
                    SiteAddressID = 4,
                    CustomerID = 6,
                    Address1 = "8 Rose Street",
                    Postcode = "M4 4FB",
                    TBLSiteAddressSiteAddressTypes = addressTypes,
                    flagHidden = false
                },

                new TBLSiteAddress()
                {
                    SiteAddressID = 5,
                    CustomerID = 7,
                    Address1 = "8 Fin Street",
                    Postcode = "M8 8FB",
                    TBLSiteAddressSiteAddressTypes = addressTypes,
                    flagHidden = false
                },

                new TBLSiteAddress()
                {
                    SiteAddressID = 6,
                    CustomerID = 8,
                    Address1 = "8 Emb Street",
                    Postcode = "M9 9FB",
                    TBLSiteAddressSiteAddressTypes = addressTypes,
                    flagHidden = false
                },
                
                new TBLSiteAddress()
                {
                    SiteAddressID = 7,
                    CustomerID = 9,
                    Address1 = "8 Peter Street",
                    Postcode = "M2 2FB",
                    TBLSiteAddressSiteAddressTypes = addressTypes,
                    flagHidden = false
                },
                
                new TBLSiteAddress()
                {
                    SiteAddressID = 7,
                    CustomerID = 10,
                    Address1 = "8 Peter Street",
                    Postcode = "M2 2FB",
                    TBLSiteAddressSiteAddressTypes = addressTypes,
                    flagHidden = false
                },
                
                new TBLSiteAddress()
                {
                    SiteAddressID = 7,
                    CustomerID = 11,
                    Address1 = "8 Peter Street",
                    Postcode = "M2 2FB",
                    TBLSiteAddressSiteAddressTypes = addressTypes,
                    flagHidden = false
                },
                
                new TBLSiteAddress()
                {
                    SiteAddressID = 7,
                    CustomerID = 12,
                    Address1 = "8 Peter Street",
                    Postcode = "M2 2FB",
                    TBLSiteAddressSiteAddressTypes = addressTypes,
                    flagHidden = false
                },
                
                new TBLSiteAddress()
                {
                    SiteAddressID = 7,
                    CustomerID = 13,
                    Address1 = "8 Peter Street",
                    Postcode = "M2 2FB",
                    TBLSiteAddressSiteAddressTypes = addressTypes,
                    flagHidden = false
                },

                new TBLSiteAddress()
                {
                    SiteAddressID = 7,
                    CustomerID = 14,
                    Address1 = "8 Peter Street",
                    Postcode = "M2 2FB",
                    TBLSiteAddressSiteAddressTypes = addressTypes,
                    flagHidden = false
                }
            };

            

            var dbSetMockCustomer = DbSetInitialisedMockFactory<TBLCustomer>.CreateDbSetInitalisedMock(customerData);
            _peninsulaEntities.Setup(x => x.TBLCustomers).Returns(dbSetMockCustomer.Object);

            var dbSetMockAddressType = DbSetInitialisedMockFactory<TBLSiteAddressSiteAddressType>.CreateDbSetInitalisedMock(addressTypes);
            _peninsulaEntities.Setup(x => x.TBLSiteAddressSiteAddressTypes).Returns(dbSetMockAddressType.Object);

            var dbSetMockAddress = DbSetInitialisedMockFactory<TBLSiteAddress>.CreateDbSetInitalisedMock(addresses);
            _peninsulaEntities.Setup(x => x.TBLSiteAddresses).Returns(dbSetMockAddress.Object);

            TblCustomerRepository = new TblCustomerRepository(_dbContextManager);
        }
    }
}
