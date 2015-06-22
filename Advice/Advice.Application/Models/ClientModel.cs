using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Application.Models
{
    public class ClientModel
    {
        public ClientModel(int clientId, string can, string clientName, string postCode)
        {
            ClientId = clientId;
            Can = can;
            ClientName = clientName;
            PostCode = postCode;
        }

        public int ClientId { get; private set; }
        public string Can { get; private set; }
        public string ClientName { get; private set; }
        public string PostCode { get; private set; }
    }
}
