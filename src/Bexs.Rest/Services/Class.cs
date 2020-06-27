using Bexs.Rest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bexs.Rest.Services
{
    public class RouteService
    {
        private Dictionary<string, int> RouteEntrys = new Dictionary<string, int>();
        private List<Entities.Route> Routes => new List<Entities.Route>()
        {
            new Route()
            {
                From = 1,
                To = 2
            },
            new Route()
            {
                From = 3,
                To = 1
            },
            new Route()
            {
                From = 4,
                To = 3
            }
        };

        private void CheapestRoute(string fromCode, string toCode)
        {
            if (!RouteEntrys.TryGetValue(fromCode, out int from))
                return;

            if (!RouteEntrys.TryGetValue(toCode, out int to))
                return;

        }

        public List<Entities.Route> CheapeastRoute(int to, int from)
        {
            var possibleRoutes = Routes.Where(x => x.To == to).ToList();

            var route = possibleRoutes.OrderByDescending(x => x.Price).FirstOrDefault(x => x.From == from);

            if (route != null)
                return new List<Entities.Route> { route };

            var routesToSearch = possibleRoutes.Select(x=> new RotaProcesso
            {
                From = x.From,
                Id = x.Id,
                OriginalFrom = from,
                PastRoutes = new List<Route>
                {
                    x
                },
                Price = x.Price,
                To = x.To
            });

            var rotas = routesToSearch.Select(x => CheapeastRoute(x)).OrderByDescending(x => x.PastRoutes.Sum(y => y.Price)).FirstOrDefault().PastRoutes;

            return rotas;
        }

        public Entities.RotaProcesso CheapeastRoute(RotaProcesso searchRoute)
        {
            var possibleRoutes = Routes.Where(x => x.To == searchRoute.From).ToList();

            var route = possibleRoutes.OrderByDescending(x => x.Price).FirstOrDefault(x => x.From == searchRoute.OriginalFrom);

            if (route != null)
            {
                var result = new RotaProcesso
                {
                    PastRoutes = searchRoute.PastRoutes
                };

                result.PastRoutes.Add(route);

                return result;
            }

            var routesToSearch = possibleRoutes.Select(x => new RotaProcesso
            {
                From = x.From,
                Id = x.Id,
                OriginalFrom = searchRoute.OriginalFrom,
                PastRoutes = searchRoute.PastRoutes,
                Price = x.Price,
                To = x.To
            }).ToList();

            routesToSearch.ForEach(x => x.PastRoutes.Add(searchRoute));

            var rota = routesToSearch.Select(x => CheapeastRoute(x)).OrderByDescending(x => x.PastRoutes.Sum(y => y.Price)).FirstOrDefault();

            return rota;
        }

    }
}
