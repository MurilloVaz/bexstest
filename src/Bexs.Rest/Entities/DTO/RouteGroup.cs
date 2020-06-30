using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bexs.Rest.Entities.DTO
{
    public class RouteGroup
    {
        public IEnumerable<Route> Routes { get; set; }

        public string Route { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
