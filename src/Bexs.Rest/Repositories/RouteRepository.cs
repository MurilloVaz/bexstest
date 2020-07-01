using Bexs.Rest.Entities;
using Bexs.Rest.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Noty;
using Noty.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bexs.Rest.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly SqlServerContext _ctx;
        private readonly IMemoryCache _cache;
        public RouteRepository(SqlServerContext ctx, IMemoryCache cache)
        {
            _ctx = ctx;
            _cache = cache;
        }

        public async Task<Dictionary<string, int>> GetRouteEntries()
        {
            return await _cache.GetOrCreateAsync("GetRouteEntries", async entry =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromSeconds(10));
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20);

                var result = await _ctx.ExecuteStoredProcedureCollectionReader<RouteEntry>("USP_Select_RouteEntry");

                return result?.ToDictionary(x => x.Code, x => x.Id);
            });
        }

        public async Task<IEnumerable<Route>> GetRoutes()
        {
            return await _cache.GetOrCreateAsync("GetRoutes", entry =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromSeconds(10));
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20);

                return _ctx.ExecuteStoredProcedureCollectionReader<Route>("USP_Select_Route");
            });
        }

        public async Task<bool> InsertRoute(Entities.DTO.BaseRoute baseRoute)
        {
            var result = await _ctx.ExecuteStoredProcedureScalar<object>("USP_Insert_Route",
                                                                "fromCode".WithValue(baseRoute.From),
                                                                "toCode".WithValue(baseRoute.To),
                                                                "price".WithValue(baseRoute.Price)
                                                                );

            if (result == null || Convert.ToInt32(result) <= 0)
                return false;

            return true;
        }

    }
}
