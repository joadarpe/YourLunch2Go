using System.IO;
using System.Linq;
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
            IDroneDeliveryService droneDeliveryService = new DroneDeliveryService();
            droneDeliveryService.DeliverOrders(workingPath, workingPath);

            var outFiles = Directory.GetFiles(workingPath, "out*.txt").Select(fn => new FileInfo(fn));

            // Then
            Assert.NotNull(outFiles);
            Assert.Equal(expectedfilesInfo.Count(), outFiles.Count());
            outFiles.ToList().ForEach(AssertFileContent);
        }

        private static void AssertFileContent(FileInfo fI)
        {
            Assert.Equal(
                File.ReadAllLines(fI.FullName.Replace(fI.Name, $"expected-{fI.Name}")),
                File.ReadAllLines(fI.FullName));
        }
    }
}
