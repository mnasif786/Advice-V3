using System.Web.Http.Controllers;
using Advice.Web.Helpers;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Advice.Web
{
    public class ControllerRegistry : Registry
    {
        public ControllerRegistry()
        {
            For<IUserIdentityFactory>().Use<UserIdentityFactory>();
        } 
    }
}