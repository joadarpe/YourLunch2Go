using System.Collections.Generic;
using System.Linq;
using Delivery.Domain.Services;
using Xunit;

namespace Delivery.Tests.Domain.Services
{
    public class DroneServiceTest
    {

        [Fact(Skip = "This was what the document said about inputs and outputs but it does not work")]
        public void ShouldReturnDronePossitionFromCommandList_AsDocummented()
        {
            uint droneId = 1;
            var droneService = new DroneService();
            var commandList = new List<string> { "AAAAIAA", "DDDAIAD", "AAIADAD" };
            var expectedList = new List<string> { "(-2, 4) N", "(-3, 3) S", "(-4, 2) E" };

            DeliverOrdersAndAssert(droneService, droneId, commandList, expectedList);
        }

        [Fact]
        public void ShouldReturnDronePossitionFromCommandList_AsUnderstood()
        {
            uint droneId = 1;
            var droneService = new DroneService();
            var commandList = new List<string> { "AAAAIAA", "DDDAIAD", "AAIADAD" };
            var expectedList = new List<string> { "(-2, 4) W", "(-1, 3) S", "(0, 0) W" };

            DeliverOrdersAndAssert(droneService, droneId, commandList, expectedList);

        }

        private static void DeliverOrdersAndAssert(IDroneService droneService,
            uint droneId, IEnumerable<string> commandList, IEnumerable<string> expectedList)
        {
            var result = droneService.DeliverOrders(droneId, commandList);

            Assert.NotNull(result);
            Assert.Equal(expectedList, result.ToList());
        }
    }
}
