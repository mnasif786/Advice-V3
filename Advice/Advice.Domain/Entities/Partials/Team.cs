using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Advice.Domain.Entities
{
    public partial class Team
    {
        public void EditTeam(string description, int divisionId, int departmentId, string userName)
        {

            Description = description;
            DivisionId = divisionId <= 0 ? (int?)null : divisionId;
            DepartmentID = departmentId;
            LastModifiedBy = userName;
            LastModifiedDate = DateTime.Now;
        }

        public void DeleteTeam(string userName)
        {
            Deleted = true;
            DeletedBy = userName;
            DeletedDate = DateTime.Now;
            LastModifiedBy = userName;
            LastModifiedDate = DateTime.Now;
        }

        public void ReinstateTeam(string userName)
        {
            Deleted = false;
            DeletedBy = null;
            DeletedDate = null;
            LastModifiedBy = userName;
            LastModifiedDate = DateTime.Now;
        }

        public static Team Create(string description, int divisionId, int departmentId, string userName)
        {
            var team = new Team
            {
                Description = description,
                DivisionId = divisionId <= 0 ? (int?) null : divisionId,
                DepartmentID = departmentId,
                CreatedBy = userName,
                CreatedDate = DateTime.Now,
                LastModifiedBy = userName,
                LastModifiedDate = DateTime.Now,
                ManagerID = 6 // hardcoded value: apparantly this field is not used for anything and it is mandatory. 6 is the most used value in the table
            };

            return team;
        } 
    }
}
