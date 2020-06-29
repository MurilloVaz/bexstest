using Bexs.Rest.Entities;
using Bexs.Rest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bexs.Rest.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _repo;
        private readonly Dictionary<string, int> _routeEntries = new Dictionary<string, int>();
        private readonly List<Entities.Route> _routes;

        public RouteService(IRouteRepository repo)
        {
            _routes = repo.GetRoutes().GetAwaiter().GetResult()?.ToList();
            _routeEntries = repo.GetRouteEntries().GetAwaiter().GetResult();
            _repo = repo;
        }

        public async Task<bool> InsertRoute(Entities.DTO.BaseRoute newRoute)
        {
            return await _repo.InsertRoute(newRoute);
        }

        public Entities.DTO.RouteGroup CheapestRoute(string fromCode, string toCode)
        {
            if (!_routeEntries.TryGetValue(fromCode, out int from))
                return null;

            if (!_routeEntries.TryGetValue(toCode, out int to))
                return null;

            var routes = CheapestRoute(from, to);

            if (routes == null)
                return null;

            return MapToDto(routes);
        }

        public List<Entities.Route> CheapestRoute(int from, int to)
        {
            SearchForRoute(from, to, out Route route, out List<Route> possibleRoutes);

            if (route != null)
                return new List<Entities.Route> { route };

            var routesToSearch = possibleRoutes.Select(x=> new RouteToSearch
            {
                CurrentRoute = x,
                PastRoutes = new List<Route>(),
                OriginalFrom = from
            });

            var routesMatched = routesToSearch.Select(x => CheapestRoute(x))?
                                .OrderByDescending(x => x?.PastRoutes?.Sum(y => y.Price))
                                .FirstOrDefault()?.PastRoutes;

            routesMatched?.Reverse();

            return routesMatched;
        }

        private Entities.RouteToSearch CheapestRoute(RouteToSearch searchRoute)
        {
            searchRoute.PastRoutes.Add(searchRoute.CurrentRoute);

            SearchForRoute(searchRoute.OriginalFrom, searchRoute.CurrentRoute.From, out Route route, out List<Route> possibleRoutes);

            if (route != null)
            {
                searchRoute.PastRoutes.Add(route);

                return searchRoute;
            }

            var routesToSearch = possibleRoutes.Select(x => new RouteToSearch
            {
                CurrentRoute = x,
                PastRoutes = searchRoute.PastRoutes,
                OriginalFrom = searchRoute.OriginalFrom
            }).ToList();

            return routesToSearch.Select(x => CheapestRoute(x))
                                        .OrderByDescending(x => x?.PastRoutes?.Sum(y => y.Price))
                                        .FirstOrDefault();
        }

        private void SearchForRoute(int from, int to, out Route route, out List<Route> possibleRoutes)
        {
             possibleRoutes = _routes.Where(x => x.To == to).ToList();
             route = possibleRoutes.FirstOrDefault(x => x.From == from);
        }

        private Entities.DTO.RouteGroup MapToDto(List<Entities.Route> routes)
        {
            return new Entities.DTO.RouteGroup()
            {
                Routes = routes.Select((x, i) => new Entities.DTO.Route {
                    From = x.FromCode,
                    Order = i,
                    Price = x.Price,
                    To = x.ToCode
                }),
                Price = routes.Sum(x => x.Price)
            };
        }

    }
}
