using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public abstract class BaseTblSiteAddressRepositoryTests
    {
        private IPeninsulaDbContextManager _dbContextManager;
        private Mock<PeninsulaEntities> _peninsulaEntities;
        protected ITBLSiteAddressRepository TblSiteAddressRepository;

        [SetUp]
        public void BaseFixtureSetup()
        {
            _peninsulaEntities = new Mock<PeninsulaEntities>();
            _dbContextManager = new PeninsulaDbContextManager(_peninsulaEntities.Object);

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
                },
                
                new TBLSiteAddressSiteAddressType()
                {
                    SiteAddressID = 8,
                    SiteAddressTypeID = 3,
                }
            };

            var addresses = new List<TBLSiteAddress>()
            {
                
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
                    SiteAddressID = 8,
                    CustomerID = 10,
                    Address1 = "8 Peter Street",
                    Postcode = "M2 2FB",
                    TBLSiteAddressSiteAddressTypes = addressTypes,
                    flagHidden = false
                }
            };

            int i = 0;
            foreach (var tblSiteAddressSiteAddressType in addressTypes)
            {
                tblSiteAddressSiteAddressType.TBLSiteAddress = addresses[i++];
            }

            var dbSetMockAddressType = DbSetInitialisedMockFactory<TBLSiteAddressSiteAddressType>.CreateDbSetInitalisedMock(addressTypes);
            _peninsulaEntities.Setup(x => x.TBLSiteAddressSiteAddressTypes).Returns(dbSetMockAddressType.Object);

            var dbSetMockAddress = DbSetInitialisedMockFactory<TBLSiteAddress>.CreateDbSetInitalisedMock(addresses);
            _peninsulaEntities.Setup(x => x.TBLSiteAddresses).Returns(dbSetMockAddress.Object);

            TblSiteAddressRepository = new TBLSiteAddressRepository(_dbContextManager);
        }
    }
}
