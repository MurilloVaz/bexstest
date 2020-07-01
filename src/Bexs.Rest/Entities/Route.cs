using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bexs.Rest.Entities
{
    public class Route
    {
        public int Id { get; set; }

        public int From { get; set; }

        public string FromCode { get; set; }

        public int To { get; set; }

        public string ToCode { get; set; }

        public decimal Price { get; set; }
    }
}
