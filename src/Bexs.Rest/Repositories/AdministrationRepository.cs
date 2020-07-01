using Bexs.Rest.Interfaces;
using Noty;
using Noty.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bexs.Rest.Repositories
{
    public class AdministrationRepository : IAdministrationRepository
    {
        private readonly SqlServerContext _ctx;
        public AdministrationRepository(SqlServerContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> PackageIsRunning(string packageName)
        {
            var result = await _ctx.ExecuteStoredProcedureScalar<object>("USP_Select_SSISExecution",
                                                                "@package".WithValue(packageName)
                                                                );

            if (result == null || Convert.ToInt32(result) <= 0)
                return false;

            return true;
        }

        public async Task<bool> RunPackage(string packageName)
        {
            var result = await _ctx.ExecuteStoredProcedureScalar<object>("USP_Start_SSISExecution",
                                                                "@package".WithValue(packageName)
                                                                );

            if (result == null || Convert.ToInt64(result) <= 0)
                return false;

            return true;
        }
    }
}
