using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using StructureMap;
using IContainer = StructureMap.IContainer;


namespace Advice.ServiceReviews.Common
{
    public static class IocConfig
    {
        public static IContainer Setup()
        {

            var container = new StructureMap.Container(

                x =>
                {
                    //x.For<IImpersonator>().Use<Impersonator>();

                    
                    x.AddRegistry<JobRegistry>();

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
