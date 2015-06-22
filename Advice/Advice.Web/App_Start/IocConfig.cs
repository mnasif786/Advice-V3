using Advice.Application.Common;
using Advice.Infrastructure;
using Advice.Infrastructure.Security;
using StructureMap;


namespace Advice.Web.App_Start
{
    public static class IocConfig
    {
        public static IContainer Setup()
        {
           
            var container = new Container(
               
                x =>
                {
                    x.For<IImpersonator>().Use<Impersonator>();
            
                    x.AddRegistry<ControllerRegistry>();
                    x.AddRegistry<ApplicationRegistry>();

                    x.Scan(y =>
                    {
                        y.WithDefaultConventions();
                        y.LookForRegistries();
                        
                    });
                }
        );

            return container;
        }
    }
}