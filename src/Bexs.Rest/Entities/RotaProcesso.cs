using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bexs.Rest.Entities
{
    public class RotaProcesso : Route
    {
        public List<Route> PastRoutes { get; set; }

        public int OriginalFrom { get; set; }
    }
}
