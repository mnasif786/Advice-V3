using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.EmployeeImport.EventHandlers;
using HrOnline.MessageContracts.Employee.Events;
using NUnit.Framework;

namespace Advice.EmployeeImportTests
{
    public class EmployeeUpdateEventHandlerTests
    { 
        private EmployeeUpdated _message;   

        [SetUp] 
        public void SetUp() 
        { 
            _message = new EmployeeUpdated { DateOfBirth = DateTime.Parse("18/09/1992"), Forename = "Nauman", Surname = "Asif", Initial = "M", Gender = "Male"};

        } 

        private EmployeeUpdateEventHandler CreateTarget() 
        { 
            var handler = new EmployeeUpdateEventHandler(); 
            return handler; 
        }


    }
}
