using Advice.ServiceReviews.Contracts;
using Advice.ServiceReviews.Implementations;
using StructureMap.Configuration.DSL;

namespace Advice.ServiceReviews.Common
{
    public class JobRegistry : Registry
    {
        public JobRegistry()
        {
            For<IServiceReviewJob>().Use<ServiceReviewJob>();
            For<IServiceReviewService>().Use<ServiceReviewService>();

            Configure(x => x.ImportRegistry(typeof(AdviceDataRegistry)));
            Configure(x => x.ImportRegistry(typeof(PeninsulaDataRegistry)));
        }
    }
}
