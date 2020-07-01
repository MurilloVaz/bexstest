using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bexs.Rest.Interfaces
{
    public interface IAdministrationRepository
    {
        Task<bool> PackageIsRunning(string packageName);
        Task<bool> RunPackage(string packageName);
    }
}
