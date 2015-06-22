using System;
using HrOnline.MessageContracts.Employee.Events;
using NServiceBus;

namespace Advice.EmployeeImport.EventHandlers
{
    public class EmployeeUpdateEventHandler : IHandleMessages<EmployeeUpdated>
    {
        public void Handle(EmployeeUpdated message)
        {
            throw new System.NotImplementedException();
        }
    }
}
