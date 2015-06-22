using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Common.Models
{
    public class ClientRecentActionModel
    {
        public ClientRecentActionModel(int clientId, string can, string clientName, DateTime lastAction)
        {
            ClientId = clientId;
            Can = can;
            ClientName = clientName;
            LastAction = lastAction;
        }

        public int ClientId { get; private set; }
        public string Can { get; private set; }
        public string ClientName { get; private set; }
        public DateTime LastAction { get; private set; }
    }
}
