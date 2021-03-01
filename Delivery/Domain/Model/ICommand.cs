using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.Domain.Model.Utils;

namespace Delivery.Domain.Model
{
    public interface ICommand
    {
        IDrone Execute(IDrone drone);
    }

    public class CommandFactory
    {
        public const char FORWARD = 'A';
        public const char TURN_RIGHT = 'D';
        public const char TURN_LEFT = 'I';

        public static IEnumerable<ICommand> FromString(string commands)
        {
            if (string.IsNullOrEmpty(commands))
                throw new ArgumentException("Empty command list");

            return commands.ToCharArray().Select(c => Create(c));
        }

        private static ICommand Create(char c)
        {
            return c switch
            {
                FORWARD => new ForwardCommand(),
                TURN_RIGHT => new TurnRightCommand(),
                TURN_LEFT => new TurnLeftCommand(),
                _ => throw new ArgumentException("Unknown command"),
            };
        }
    }

    public class ForwardCommand : ICommand
    {
        private const int STEPS = 1;

        public IDrone Execute(IDrone drone)
        {
            var p = drone.Position;
            var newP = p.Orientation.Value switch
            {
                CardinalPoint.N => Position.Create(p.Xaxis, p.Yaxis + STEPS, p.Orientation),
                CardinalPoint.E => Position.Create(p.Xaxis + STEPS, p.Yaxis, p.Orientation),
                CardinalPoint.S => Position.Create(p.Xaxis, p.Yaxis - STEPS, p.Orientation),
                CardinalPoint.W => Position.Create(p.Xaxis - STEPS, p.Yaxis, p.Orientation),
                _ => p
            };
            return Drone.Create(drone.Id, drone.Alias, newP);

        }
    }

    public class TurnRightCommand : ICommand
    {
        public IDrone Execute(IDrone drone)
        {
            var p = drone.Position;
            var newP = Position.Create(p.Xaxis, p.Yaxis, CardinalPoint.FromPoint(p.Orientation.Right));
            return Drone.Create(drone.Id, drone.Alias, newP);
        }
    }

    public class TurnLeftCommand : ICommand
    {
        public IDrone Execute(IDrone drone)
        {
            var p = drone.Position;
            var newP = Position.Create(p.Xaxis, p.Yaxis, CardinalPoint.FromPoint(p.Orientation.Left));
            return Drone.Create(drone.Id, drone.Alias, newP);
        }
    }
}
