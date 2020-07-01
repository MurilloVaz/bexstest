using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bexs.Rest.Interfaces
{
    public interface IRouteService
    {
        Entities.DTO.RouteGroup CheapestRoute(string fromCode, string toCode);
        List<Entities.Route> CheapestRoute(int from, int to);
        Task<bool> InsertRoute(Entities.DTO.BaseRoute newRoute);
    }
}
