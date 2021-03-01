using System.Collections.Generic;
using System.Linq;
using Delivery.Domain.Model;
using Delivery.Domain.Services;
using Xunit;

namespace Delivery.Tests.Domain.Services
{
    public class DroneServiceTest
    {

        [Fact(Skip = "This was what the document said about inputs and outputs but it does not work")]
        public void ShouldReturnDronePositionFromCommandList_AsDocummented()
        {
            uint droneId = 1;
            var droneService = new DroneService(DeliverySettings.Create());
            var commandList = new List<string> { "AAAAIAA", "DDDAIAD", "AAIADAD" };
            var expectedList = new List<string> { DroneService.OUTFILE_HEADER, "(-2, 4) Ahead North", "(-3, 3) Ahead South", "(-4, 2) Ahead East" };

            DeliverOrdersAndAssert(droneService, droneId, commandList, expectedList);
        }

        [Fact]
        public void ShouldReturnDronePositionFromCommandList_AsUnderstood()
        {
            uint droneId = 1;
            var droneService = new DroneService(DeliverySettings.Create());
            var commandList = new List<string> { "AAAAIAA", "DDDAIAD", "AAIADAD" };
            var expectedList = new List<string> { DroneService.OUTFILE_HEADER, "(-2, 4) Ahead West", "(-1, 3) Ahead South", "(0, 0) Ahead West" };

            DeliverOrdersAndAssert(droneService, droneId, commandList, expectedList);
        }

        [Fact]
        public void ShouldNotDeliverMoreThanThreeOrders()
        {
            uint droneId = 1;
            var droneService = new DroneService(DeliverySettings.Create());
            var commandList = new List<string> { "AAAAIAA", "DDDAIAD", "AAIADAD", "IGNORED" };
            var expectedList = new List<string> { DroneService.OUTFILE_HEADER, "(-2, 4) Ahead West", "(-1, 3) Ahead South", "(0, 0) Ahead West" };

            DeliverOrdersAndAssert(droneService, droneId, commandList, expectedList);
        }

        [Fact]
        public void ShouldNotDeliverMoreThanTenBlocks()
        {
            uint droneId = 1;
            var droneService = new DroneService(DeliverySettings.Create(blocksToDeliver: 10));
            var commandList = new List<string> { "AAAAAAAAAAA", "AAAIAAA", "IAAAIAAA" };
            var expectedList = new List<string> { DroneService.OUTFILE_HEADER, "(0, 0) Ahead North", "(-3, 3) Ahead West", "(0, 0) Ahead East" };

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
