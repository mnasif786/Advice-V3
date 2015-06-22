using System;
using System.Collections.Generic;
using System.Linq;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Domain.RepositoryContracts;

namespace Advice.Application.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepositary;

        public DepartmentService(IDepartmentRepository departmentRepositary)
        {
            _departmentRepositary = departmentRepositary;
        }

        public IEnumerable<DepartmentModel> GetAllDepartments()
        {
            return _departmentRepositary.GetAll().Select(d => new DepartmentModel(d.DepartmentID, d.Description));
        }
    }
}
