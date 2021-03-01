using System.IO;
using System.Linq;
using Delivery.Domain.Model;
using Delivery.Infraestructure.Services;
using Xunit;

namespace Delivery.Test.Infraestructure.Services
{
    public class DroneDeliveryServiceTest
    {

        [Fact]
        public void ShouldCreateDroneOutFileFromInFile()
        {
            // Given
            var workingPath = Path.Combine(".","Infraestructure","Services","Resources");
            var expectedfilesInfo = Directory.GetFiles(workingPath, "expected-out*.txt").Select(fn => new FileInfo(fn));

            // When
            IDroneDeliveryService droneDeliveryService = new DroneDeliveryService(DeliverySettings.Create());
            droneDeliveryService.DeliverOrders(workingPath, workingPath);

            var outFiles = Directory.GetFiles(workingPath, "out*.txt").Select(fn => new FileInfo(fn));

            // Then
            Assert.NotNull(outFiles);
            Assert.Equal(expectedfilesInfo.Count(), outFiles.Count());
            outFiles.ToList().ForEach(AssertFileContentAndDeleteOutFile);
        }

        [Fact]
        public void ShouldNotProcessMoreThanOneDrone()
        {
            // Given
            var workingPath = Path.Combine(".", "Infraestructure", "Services", "Resources");
            var expectedfilesInfo = Directory.GetFiles(workingPath, "expected-out*.txt").Select(fn => new FileInfo(fn));

            // When
            IDroneDeliveryService droneDeliveryService = new DroneDeliveryService(DeliverySettings.Create(droneCount: 1));
            droneDeliveryService.DeliverOrders(workingPath, workingPath);

            var outFiles = Directory.GetFiles(workingPath, "out*.txt").Select(fn => new FileInfo(fn));

            // Then
            Assert.NotNull(outFiles);
            Assert.Single(outFiles);
            outFiles.ToList().ForEach(AssertFileContentAndDeleteOutFile);
        }

        private static void AssertFileContentAndDeleteOutFile(FileInfo fI)
        {
            Assert.Equal(
                File.ReadAllLines(fI.FullName.Replace(fI.Name, $"expected-{fI.Name}")),
                File.ReadAllLines(fI.FullName));
            File.Delete(fI.FullName);
        }
    }
}
