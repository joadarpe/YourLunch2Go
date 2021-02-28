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

        public IEnumerable<string> DeliverOrders(uint id, IEnumerable<string> routes)
        {
            var drone = Drone.Create(id);
            var routePlan = routes.Select(r => Route.FromString(r));

            return routePlan.Select(r => drone = drone.Deliver(r))
                .Select(d => d.Possition.ToString());
        }
    }

}
