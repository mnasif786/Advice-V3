using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Advice.Domain.Entities
{
    public class CreatedUser
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public static CreatedUser Create(string name)
        {
            return new CreatedUser() { Name = name, CreatedDate = DateTime.Now };
        }
    }
}
