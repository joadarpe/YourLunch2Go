using System.Collections.Generic;
using System.Linq;
using Delivery.Domain.Model;

namespace Delivery.Domain.Services
{
    public interface IDroneService
    {
        IEnumerable<string> DeliverOrders(uint id, IEnumerable<string> routes);
    }

    public class DroneService : IDroneService
    {
        public const string OUTFILE_HEADER = "== Delivery report ==";

        private readonly IDeliverySettings _deliverySettings;

        public DroneService(IDeliverySettings deliverySettings)
        {
            _deliverySettings = deliverySettings;
        }

        public IEnumerable<string> DeliverOrders(uint id, IEnumerable<string> routes)
        {
            var drone = Drone.Create(id);

            var routePlan = routes.Select(r => Route.FromString(r)).Take(_deliverySettings.MaxOrdersByDrone);

            return routePlan.Select(r => drone = DeliverOrder(_deliverySettings, drone, r))
                .Select(d => d.Position.ToString()).Prepend(OUTFILE_HEADER);
        }

        private static IDrone DeliverOrder(IDeliverySettings deliverySettings, IDrone drone, IRoute route)
        {
            var baseDrone = drone;
            var newDrone = drone.Deliver(route);
            if (IsOutOfReach(deliverySettings, newDrone.Position))
                return baseDrone;
            return newDrone;
        }

        private static bool IsOutOfReach(IDeliverySettings deliverySettings, IPosition position)
        {
            return deliverySettings.MaxBlocksToDeliver < System.Math.Abs(position.Xaxis)
                || deliverySettings.MaxBlocksToDeliver < System.Math.Abs(position.Yaxis);
        }
    }

}
