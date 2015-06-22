using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Infrastructure.Security
{
    public interface IImpersonator : IDisposable
    {
        void ImpersonateValidUser(string userName, string domain, string encryptedPassword);
    }
}
