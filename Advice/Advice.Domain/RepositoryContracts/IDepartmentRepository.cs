using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Common;
using Advice.Domain.Entities;

namespace Advice.Domain.RepositoryContracts
{
    public interface IDepartmentRepository: IAdviceRepository<Department>
    {
        Department GetDepartmentByDepartmentId(long departmentId );
        string GetDepartmentDiscriptionByDepartmentId(long departmentId);
    }
}
