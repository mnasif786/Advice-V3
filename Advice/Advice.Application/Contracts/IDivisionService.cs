using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Common.Models;

namespace Advice.Application.Contracts
{
    public interface IDivisionService
    {
        IEnumerable<DivisionModel> GetAllDivisions();

        void UpdateDivision(DivisionModel divisionModel);

        void AddDivision(DivisionModel divisionModel, string username);
    }
}
