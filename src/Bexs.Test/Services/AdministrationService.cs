using Bexs.Rest.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bexs.Test.Services
{
    public class AdministrationService
    {
        private readonly Mock<IConfiguration> _configMockSource;
        private readonly Mock<IFormFile> _fileMockSource;

        public AdministrationService()
        {
            _configMockSource = new Mock<IConfiguration>();
            _configMockSource.SetupGet(x => x[It.Is<string>(s => s == "AppSettings:FileDirectory")]).Returns(Path.GetTempPath());

            _fileMockSource = new Mock<IFormFile>();
            var content = "FakeFile";
            var fileName = "input-csv.txt";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            _fileMockSource.Setup(_ => _.OpenReadStream()).Returns(ms);
            _fileMockSource.Setup(_ => _.FileName).Returns(fileName);
            _fileMockSource.Setup(_ => _.Length).Returns(ms.Length);
            writer.Close();
        }

        [Fact]
        public async Task UpdateDatabase_PackageIsRunning_ReturnsFalse()
        {
            var repoMockSource = new Mock<IAdministrationRepository>();
            repoMockSource.Setup(m => m.PackageIsRunning(It.IsAny<string>()))
                            .Returns(async () => true);

            IAdministrationService admService = new Rest.Services.AdministrationService(repoMockSource.Object, _configMockSource.Object);

            IFormFile fileInput = _fileMockSource.Object;

            var result = await admService.UpdateDatabase(fileInput);

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateDatabase_PackageNotRunning_ReturnsTrue()
        {
            var repoMockSource = new Mock<IAdministrationRepository>();
            repoMockSource.Setup(m => m.PackageIsRunning(It.IsAny<string>()))
                            .Returns(async () => false);

            repoMockSource.Setup(m => m.RunPackage(It.IsAny<string>()))
                            .Returns(async () => true);

            IAdministrationService admService = new Rest.Services.AdministrationService(repoMockSource.Object, _configMockSource.Object);

            IFormFile fileInput = _fileMockSource.Object;

            var result = await admService.UpdateDatabase(fileInput);

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateDatabase_PackageExecutionFail_ReturnsFalse()
        {
            var repoMockSource = new Mock<IAdministrationRepository>();
            repoMockSource.Setup(m => m.PackageIsRunning(It.IsAny<string>()))
                            .Returns(async () => false);

            repoMockSource.Setup(m => m.RunPackage(It.IsAny<string>()))
                            .Returns(async () => false);

            IAdministrationService admService = new Rest.Services.AdministrationService(repoMockSource.Object, _configMockSource.Object);

            IFormFile fileInput = _fileMockSource.Object;

            var result = await admService.UpdateDatabase(fileInput);

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateDatabase_PackageExecutionSuccess_ReturnsTrue()
        {
            var repoMockSource = new Mock<IAdministrationRepository>();
            repoMockSource.Setup(m => m.PackageIsRunning(It.IsAny<string>()))
                            .Returns(async () => false);

            repoMockSource.Setup(m => m.RunPackage(It.IsAny<string>()))
                            .Returns(async () => true);

            IAdministrationService admService = new Rest.Services.AdministrationService(repoMockSource.Object, _configMockSource.Object);

            IFormFile fileInput = _fileMockSource.Object;

            var result = await admService.UpdateDatabase(fileInput);

            Assert.True(result);
        }
    }
}
