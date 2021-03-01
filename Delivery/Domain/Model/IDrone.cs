using System.Linq;
using Delivery.Domain.Model.Utils;

namespace Delivery.Domain.Model
{
    public interface IDrone
    {
        uint Id { get; }
        string Alias { get; }
        IPosition Position { get; }

        IDrone Execute(ICommand command);
        IDrone Deliver(IRoute route);
    }

    public class Drone : IDrone
    {
        public uint Id { get; private set; }
        public string Alias { get; private set; }
        public IPosition Position { get; private set; }

        private static readonly IPosition DEFAULT = Model.Position.Create(0, 0, CardinalPoint.NORTH);

        public static IDrone Create(uint id)
        {
            return new Drone()
            {
                Id = id,
                Alias = $"ULTG_{id}",
                Position = DEFAULT
            };
        }

        public static IDrone Create(uint id, string alias, IPosition position)
        {
            return new Drone()
            {
                Id = id,
                Alias = alias,
                Position = position
            };
        }

        public IDrone Execute(ICommand command) => command.Execute(this);

        public IDrone Deliver(IRoute route)
        {
            IDrone drone = this;
            route.Commands.ToList().ForEach(c => drone = drone.Execute(c));

            return drone;
        }
    }
}
