using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Advice.Domain.Entities
{
    public class RecentCustomerAction
    {
        public int CustomerId { get; set; }
        public DateTime ActionDate { get; set; }
        
    }
}
