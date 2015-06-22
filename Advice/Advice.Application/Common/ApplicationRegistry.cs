using Advice.Application.Contracts;
using Advice.Application.Implementations;
using Advice.Data.Common;
using Advice.Infrastructure.Configuration;
using Advice.ScannedDocuments;
using Peninsula.Data.Common;
using StructureMap.Configuration.DSL;

namespace Advice.Application.Common
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            For<ITaskService>().Use<TaskService>();
            For<ITeamService>().Use<TeamService>();
            For<IUserService>().Use<UserService>();
            For<IPermissionService>().Use<PermissionService>();
            For<IDocumentService>().Use<DocumentService>();
            For<IScannedDocumentsService>().Use<ScannedDocumentsService>();
            For<IConfigurationManagerAdvice>().Use<ConfigurationManagerAdvice>();
            For<ITaskModifyingReasonService>().Use<TaskModifyingReasonService>();
            For<IClientService>().Use<ClientService>();
            For<IDepartmentService>().Use<DepartmentService>();
            For<IDivisionService>().Use<DivisionService>();
            For<IMaintenanceService>().Use<MaintenanceService>();

            Configure(x => x.ImportRegistry(typeof(AdviceDataRegistry)));
            Configure(x => x.ImportRegistry(typeof(PeninsulaDataRegistry)));
        }
    }
}
