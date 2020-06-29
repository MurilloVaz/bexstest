using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Bexs.Rest.Interfaces
{
    public interface IAdministrationService
    {
        Task<bool> UpdateDatabase(IFormFile fileInput);
    }
}
