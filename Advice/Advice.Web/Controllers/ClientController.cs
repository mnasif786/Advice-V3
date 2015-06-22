using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Advice.Application.Contracts;
using Advice.Web.Helpers;

namespace Advice.Web.Controllers
{
    [RoutePrefix("api/client")]
    public class ClientController : ApiController
    {
        private readonly IClientService _clientService;
        private readonly IUserIdentityFactory _userIdentityFactory;
        private UserIdentity LoggedInUser
        {
            get { return _userIdentityFactory.GetUserIdentity(User); }
        }
        public ClientController(IClientService clientService, IUserIdentityFactory userIdentityFactory)
        {
            _clientService = clientService;
            _userIdentityFactory = userIdentityFactory;
        }

        [Route("clientsStartWithClientName/{clientName}")]
        public IHttpActionResult GetClientsStartWithClientName(string clientName)
        {
            return Ok(_clientService.GetTop10ClientsStartWithClientName(clientName));
        }

        [Route("clientByName/{clientName}")]
        public IHttpActionResult GetClientByName(string clientName)
        {
            return Ok(_clientService.GetClientByName(clientName));
        }

        [Route("clientByCan/{can}")]
        public IHttpActionResult GetClientByCan(string can)
        {
            var client = _clientService.GetClientByCan(can);

            if (client == null)
                return  NotFound();

            return Ok(client);
        }

        [Route("clientNamesStartWith/{clientName}")]
        public IHttpActionResult GetClientNamesStartWith(string clientName)
        {
            var clientNames = _clientService.GetTop10ClientNamesStartWith(clientName);
            var clientNamesList = clientNames as IList<string> ?? clientNames.ToList();
            clientNamesList.Add("Show more..");
            return Ok(clientNamesList);
        }

        [Route("cansStartWith/{can}")]
        public IHttpActionResult GetCansStartWith(string can)
        {
            var clientNames = _clientService.GetTop10CansStartWith(can);
            var clientNamesList = clientNames as IList<string> ?? clientNames.ToList();
            clientNamesList.Add("Show more..");
            return Ok(clientNamesList);
        }

        [Route("allClientsStartWithClientName/{clientName}")]
        public IHttpActionResult GetAllClientsStartWithClientName(string clientName)
        {
            return Ok(_clientService.GetAllClientsStartWithClientName(clientName));
        }

        [Route("allClientsStartWithCan/{can}")]
        public IHttpActionResult GetAllClientsStartWithCan(string can)
        {
            return Ok(_clientService.GetAllClientsStartWithCan(can));
        }

        [Route("recentClientsAction")]
        public IHttpActionResult GetRecentClientsAction()
        {
            return Ok(_clientService.GetRecentClientsAction(LoggedInUser.Username));
        }
    }
}