using System.Web.Http;
using Advice.Web.App_Start;
using Newtonsoft.Json.Converters;
using StructureMap;

namespace Advice.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = IocConfig.Setup();
           
            GlobalConfiguration.Configuration.DependencyResolver = new DependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Convert enum values to return string values in APIs
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
        }
    }
}
