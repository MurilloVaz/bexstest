using Bexs.Rest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bexs.Rest.Interfaces
{
    public interface IRouteRepository
    {
        Task<Dictionary<string, int>> GetRouteEntries();
        Task<IEnumerable<Route>> GetRoutes();
        Task<bool> InsertRoute(Entities.DTO.BaseRoute baseRoute);

    }
}
