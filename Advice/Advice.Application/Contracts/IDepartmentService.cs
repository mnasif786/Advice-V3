using System.Collections.Generic;
using Advice.Common.Models;

namespace Advice.Application.Contracts
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentModel> GetAllDepartments();
    }
}
