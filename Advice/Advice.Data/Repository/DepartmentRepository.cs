using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.CustomExceptions;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;

namespace Advice.Data.Repository
{
    public class DepartmentRepository : AdviceRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IAdviceDbContextManager adviceDbContextManager) : base(adviceDbContextManager) { }

        public Department GetDepartmentByDepartmentId(long departmentId)
        {
            try
            {
                var department = _context.Departments.SingleOrDefault(d => d.DepartmentID == departmentId);
                return department;
            }
            catch (Exception ex)
            {
                throw new MultipleDepartmentFoundException(departmentId, ex.Message);
            }
        }

        public string GetDepartmentDiscriptionByDepartmentId(long departmentId)
        {
            try
            {
                var departmentDescription = _context.Departments.Where(d => d.DepartmentID == departmentId).Select(d => d.Description).SingleOrDefault();
                return departmentDescription;
            }
            catch (Exception ex)
            {
                throw new MultipleDepartmentFoundException(departmentId, ex.Message);
            }
        }
    }
    
}
