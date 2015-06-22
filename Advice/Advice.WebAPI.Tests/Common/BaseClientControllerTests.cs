using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Application.Contracts;
using Advice.Web.Controllers;
using Advice.Web.Helpers;
using Moq;
using NUnit.Framework;

namespace Advice.WebAPI.Tests.Common
{
    public abstract class BaseClientControllerTests
    {
        protected Mock<IClientService> ClientServiceMock;
        protected Mock<IUserIdentityFactory> UserIdentityFactory;
        protected ClientController ClientController;

        [SetUp]
        public void BaseSetUp()
        {
            ClientServiceMock = new Mock<IClientService>();
            UserIdentityFactory = new Mock<IUserIdentityFactory>();
            ClientController = GetTarget();
        }

        private ClientController GetTarget()
        {
            return new ClientController(ClientServiceMock.Object, UserIdentityFactory.Object);
        }
    }
}
