using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DavidSimmons.Repository
{
    public abstract class RepositoryBase
    {
        public string GetProcessingKey()
        {
            return string.Concat(RoleEnvironment.CurrentRoleInstance.Id, ":", Thread.CurrentThread.Name);
        }
    }
}
