using System;
using System.IO;
using System.Linq;
using Delivery.Domain.Services;

namespace Delivery.Infraestructure.Services
{
    public interface IDroneDeliveryService
    {
        void DeliverOrders(string inFilePath, string outFilePath);
    }

    public class DroneDeliveryService : IDroneDeliveryService
    {
        public void DeliverOrders(string inFilePath, string outFilePath)
        {
            var filesInfo = Directory.GetFiles(inFilePath, "in*.txt").Select(fn => new FileInfo(fn)).ToList();

            IDroneService droneService = new DroneService();

            filesInfo.ForEach(fi => DeliverOrdersForFile(droneService, fi));
        }

        private static void DeliverOrdersForFile(IDroneService dS, FileInfo fI)
        {
            var output = dS.DeliverOrders(GetDroneIdFromFileName(fI.Name), File.ReadAllLines(fI.FullName));
            File.WriteAllLines(fI.FullName.Replace(fI.Name, fI.Name.Replace("in", "out")), output);
        }

        private static uint GetDroneIdFromFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("");

            if (uint.TryParse(fileName.Substring(2, 2), out uint id))
                return id;
            throw new ArgumentException("");
        }
    }
}
