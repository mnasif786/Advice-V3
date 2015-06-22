using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Common.Models;

namespace Advice.Application.Contracts
{
    public interface IClientService
    {
        IEnumerable<string> GetTop10CansStartWith(string can);
        IEnumerable<string> GetTop10ClientNamesStartWith(string clientName);
        IEnumerable<ClientModel> GetAllClientsStartWithClientName(string clientName);
        IEnumerable<ClientModel> GetAllClientsStartWithCan(string can);
        ClientModel GetClientByName(string clientName);
        ClientModel GetClientByCan(string can);
        IEnumerable<ClientModel> GetTop10ClientsStartWithClientName(string clientName);
        IEnumerable<ClientRecentActionModel> GetRecentClientsAction(string userName);
        IEnumerable<string> GetCansStartWith(string can, short taskeRecords);
    }
}
