using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Advice.Domain.Entities
{
    public partial class Job
    {
        public void MarkAsProActiveCallBackCreated()
        {
            ProActiveCallBackCreated = true;
        }
    }
}
