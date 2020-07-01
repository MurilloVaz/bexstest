using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bexs.Rest.Entities.DTO
{
    public class BaseRoute
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Price { get; set; }
    }
}
