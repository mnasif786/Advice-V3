using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.ServiceReviews.Common;
using Advice.ServiceReviews.Contracts;

namespace Advice.ServiceReviews
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = IocConfig.Setup();
            var job = container.GetInstance<IServiceReviewJob>();
            job.Run();

        }
    }
}
