using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.Repository;
using Advice.Data.Repository.Services;
using Advice.Domain.RepositoryContracts;
using Advice.Domain.RepositoryContracts.Services;
using StructureMap.Configuration.DSL;

namespace Advice.ServiceReviews.Common
{
    public class AdviceDataRegistry : Registry
    {
        public AdviceDataRegistry()
        {
            For<IAdviceDbContextManager>().Use<AdviceDbContextManager>();
            For<ICorporatePriorityRepository>().Use<CorporatePriorityRepository>();
            For<ITaskRepository>().Use<TaskRepository>();
            For<IUserRepository>().Use<UserRepository>();
            For<IServiceReviewServiceJobScheduleRepository>().Use<ServiceReviewServiceJobScheduleRepository>();
            For<ITaskStoredProcRunner>().Use<TaskStoredProcRunner>();
            
            // UNCOMMENT FOR DEV TESTING
            // TODO: put behind config switch
            //For<IExchangeEmailService>().Use<MockExchangeEmailService>();
        }
    }
}
