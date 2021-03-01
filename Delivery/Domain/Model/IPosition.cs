using Delivery.Domain.Model.Utils;

namespace Delivery.Domain.Model
{
    public interface IPosition
    {
        int Xaxis { get; }
        int Yaxis { get; }
        CardinalPoint Orientation { get; }
    }

    public class Position : IPosition
    {

        public int Xaxis { get; private set; }
        public int Yaxis { get; private set; }
        public CardinalPoint Orientation { get; private set; }

        public override string ToString()
        {
            return $"({Xaxis}, {Yaxis}) Ahead {Orientation.Name}";
        }

        public static IPosition Create(int xAxis, int yAxis, CardinalPoint orientation) => new Position()
        {
            Xaxis = xAxis,
            Yaxis = yAxis,
            Orientation = orientation
        };
    }
}