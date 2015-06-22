using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Advice.Domain.Entities
{
    public class ModifiedUser
    {
        public string Name { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public static ModifiedUser Create(string name)
        {
            return new ModifiedUser() { Name = name, LastModifiedDate = DateTime.Now };
        }

    }
}
