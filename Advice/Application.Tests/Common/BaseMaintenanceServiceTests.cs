using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Application.Contracts;
using Advice.Application.Implementations;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using Peninsula.Domain.RepositoryContracts;

namespace Application.Tests.Common
{
    public abstract class BaseMaintenanceServiceTests
    {
        protected Mock<ICorporatePriorityRepository> CorporatePriorityRepositoryMock;
        protected Mock<ITblCustomerRepository> TblCustomerRepositoryMock;
        protected Mock<IUserRepository> UserRepositoryMock;
        protected Mock<IMaintenanceUserPermissionRepository> MaintenanceUserPermissionRepositoryMock;
        protected IMaintenanceService MaintenanceService;

        [SetUp]
        public void BaseSetUp()
        {
            CorporatePriorityRepositoryMock = new Mock<ICorporatePriorityRepository>();
            TblCustomerRepositoryMock = new Mock<ITblCustomerRepository>();
            UserRepositoryMock = new Mock<IUserRepository>();
            MaintenanceUserPermissionRepositoryMock = new Mock<IMaintenanceUserPermissionRepository>();
            MaintenanceService = new MaintenanceService(CorporatePriorityRepositoryMock.Object, TblCustomerRepositoryMock.Object, UserRepositoryMock.Object, MaintenanceUserPermissionRepositoryMock.Object);
        }
    }
}
