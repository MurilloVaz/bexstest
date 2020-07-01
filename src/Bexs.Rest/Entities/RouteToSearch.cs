using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bexs.Rest.Entities
{
    public class RouteToSearch
    {
        public int OriginalFrom { get; set; }
        public List<Route> PastRoutes { get; set; }
        public Route CurrentRoute { get; set; }
    }
}
