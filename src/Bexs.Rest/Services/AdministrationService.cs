using Bexs.Rest.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bexs.Rest.Services
{
    public class AdministrationService: IAdministrationService
    {
        private readonly string _packageName = "UpdateDatabase.dtsx";
        private readonly string _directory;
        private readonly IAdministrationRepository _repo;
        public AdministrationService(IAdministrationRepository repo, IConfiguration config)
        {
            _repo = repo;
            _directory = config["AppSettings:FileDirectory"];
        }
        public async Task<bool> UpdateDatabase(IFormFile fileInput)
        {
            if (await _repo.PackageIsRunning(_packageName))
                return false;

            using (var fs = new FileStream(Path.Combine(_directory, "input-file.txt"), FileMode.Create))
                await fileInput.CopyToAsync(fs);

            if (await _repo.RunPackage(_packageName)) 
                return true;

            return false;
        }
    }
}
