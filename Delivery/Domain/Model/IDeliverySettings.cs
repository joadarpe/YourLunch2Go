namespace Delivery.Domain.Model
{
    public interface IDeliverySettings
    {
        int MaxDroneCount { get; }
        int MaxBlocksToDeliver { get; }
        int MaxOrdersByDrone { get; }
    }

    public class DeliverySettings : IDeliverySettings
    {
        public int MaxDroneCount { get; private set; }
        public int MaxBlocksToDeliver { get; private set; }
        public int MaxOrdersByDrone { get; private set; }

        public static IDeliverySettings Create(int droneCount = 20, int blocksToDeliver = 10, int ordersByDrone = 3)
        {
            return new DeliverySettings()
            {
                MaxDroneCount = droneCount,
                MaxBlocksToDeliver = blocksToDeliver,
                MaxOrdersByDrone = ordersByDrone
            };
        }
    }
}
