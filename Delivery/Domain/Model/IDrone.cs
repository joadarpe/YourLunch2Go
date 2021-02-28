using System.Linq;
using Delivery.Domain.Model.Utils;

namespace Delivery.Domain.Model
{
    public interface IDrone
    {
        uint Id { get; }
        string Alias { get; }
        IPossition Possition { get; }

        IDrone Execute(ICommand command);
        IDrone Deliver(IRoute commandList);
    }

    public class Drone : IDrone
    {
        public uint Id { get; private set; }
        public string Alias { get; private set; }
        public IPossition Possition { get; private set; }

        private static readonly IPossition DEFAULT = Model.Possition.Create(0, 0, CardinalPoint.NORTH);

        public static IDrone Create(uint id)
        {
            return new Drone()
            {
                Id = id,
                Alias = $"ULTG_{id}",
                Possition = DEFAULT
            };
        }

        public static IDrone Create(uint id, string alias, IPossition possition)
        {
            return new Drone()
            {
                Id = id,
                Alias = alias,
                Possition = possition
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
