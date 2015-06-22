using Advice.Data.Contracts;
using Advice.Data.Repository;
using Advice.Domain.RepositoryContracts;
using Advice.ExchangeEmails;
using Registry = StructureMap.Configuration.DSL.Registry;

namespace Advice.Data.Common
{
    public class AdviceDataRegistry : Registry
    {
        public AdviceDataRegistry()
        {
            For<IAdviceDbContextManager>().Use<AdviceDbContextManager>();
            For<ITaskRepository>().Use<TaskRepository>();
            For<IUserRepository>().Use<UserRepository>();
            For<ITeamRepository>().Use<TeamRepository>();
            For<IDepartmentRepository>().Use<DepartmentRepository>();
            For<IPermissionRepository>().Use<PermissionRepository>();
            For<IExchangeEmailService>().Use<ExchangeEmailService>();
            For<ITaskModifyingReasonRepository>().Use<TaskModifyingReasonRepository>();
            For<ITaskArchiveRepository>().Use<TaskArchiveRepository>();
            For<ICustomEntityRepository>().Use<CustomEntityRepository>();
            For<IDivisionRepository>().Use<DivisionRepository>();
            For<ICorporatePriorityRepository>().Use<CorporatePriorityRepository>();
            For<IMaintenanceUserPermissionRepository>().Use<MaintenanceUserPermissionRepository>();
            For<ITaskStoredProcRunner>().Use<TaskStoredProcRunner>();
            // UNCOMMENT FOR DEV TESTING
            // TODO: put behind config switch
            //For<IExchangeEmailService>().Use<MockExchangeEmailService>();
        }
    }
}
