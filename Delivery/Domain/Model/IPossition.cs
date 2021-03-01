using Delivery.Domain.Model.Utils;

namespace Delivery.Domain.Model
{
    public interface IPossition
    {
        int Xaxis { get; }
        int Yaxis { get; }
        CardinalPoint Orientation { get; }
    }

    public class Possition : IPossition
    {

        public int Xaxis { get; private set; }
        public int Yaxis { get; private set; }
        public CardinalPoint Orientation { get; private set; }

        public override string ToString()
        {
            return $"({Xaxis}, {Yaxis}) Ahead {Orientation.Name}";
        }

        public static IPossition Create(int xAxis, int yAxis, CardinalPoint orientation) => new Possition()
        {
            Xaxis = xAxis,
            Yaxis = yAxis,
            Orientation = orientation
        };
    }
}