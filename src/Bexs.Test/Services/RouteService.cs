using Bexs.Rest.Entities;
using Bexs.Rest.Interfaces;
using Bexs.Rest.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Bexs.Test
{
    public class RouteService
    {
        private readonly Mock<IRouteRepository> _mockRouteRepository;

        public RouteService()
        {
            _mockRouteRepository = new Mock<IRouteRepository>();
            _mockRouteRepository.Setup(m => m.GetRouteEntries()).Returns(async () => new Dictionary<string, int>() {
                {"CDG", 1 },
                {"GRU", 2 },
                {"ORL", 3 },
                {"SCL", 4 },
                {"LIB", 5 },
                {"BIQ", 6 },
                {"BRC", 7 }
            });

            _mockRouteRepository.Setup(m => m.GetRoutes()).Returns(async () => new List<Route>() {
                new Route(){ Id = 1, To = 2, From = 1,FromCode = "CDG",ToCode = "GRU",Price = 75},
                new Route(){ Id = 2, To = 2, From = 3,FromCode = "ORL",ToCode = "GRU",Price = 56},
                new Route(){ Id = 3, To = 4, From = 3,FromCode = "ORL",ToCode = "SCL",Price = 20},
                new Route(){ Id = 4, To = 2, From = 4,FromCode = "SCL",ToCode = "GRU",Price = 20},
                new Route(){ Id = 5, To = 6, From = 5,FromCode = "LIB",ToCode = "BIQ",Price = 12},
                new Route() { Id = 6, To = 2, From = 7,FromCode = "BRC",ToCode = "GRU",Price = 10},
                new Route() { Id = 7, To = 3, From = 1,FromCode = "CDG",ToCode = "ORL",Price = 5},
                new Route() { Id = 8, To = 7, From = 4,FromCode = "SCL",ToCode = "BRC",Price = 5}
            });
        }




        [Fact]
        public async Task InsertRoute_SuccessInsertion_ReturnsTrue()
        {
            var mockSource = new Mock<IRouteRepository>();
            _mockRouteRepository.Setup(m => m.InsertRoute(It.IsAny<Rest.Entities.DTO.BaseRoute>()))
                .Returns(async () => true);

            IRouteService routeService = new Rest.Services.RouteService(mockSource.Object);

            var argument = new Rest.Entities.DTO.BaseRoute();

            var result = await routeService.InsertRoute(argument);

            Assert.True(result);
        }

        [Fact]
        public async Task InsertRoute_FailInsertion_ReturnsFalse()
        {
            var mockSource = new Mock<IRouteRepository>();
            _mockRouteRepository.Setup(m => m.InsertRoute(It.IsAny<Rest.Entities.DTO.BaseRoute>()))
                .Returns(async () => false);

            IRouteService routeService = new Rest.Services.RouteService(mockSource.Object);

            var argument = new Rest.Entities.DTO.BaseRoute();

            var result = await routeService.InsertRoute(argument);

            Assert.False(result);
        }


    }
}


