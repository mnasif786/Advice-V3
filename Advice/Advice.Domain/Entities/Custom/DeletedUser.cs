using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Advice.Domain.Entities
{
    public class DeletedUser
    {
        public string Name { get; set; }
        public DateTime? DeletedDate { get; set; }

        public static DeletedUser Create(string name)
        {
            return new DeletedUser() { Name = name, DeletedDate = DateTime.Now };
        }
    }
 
}
