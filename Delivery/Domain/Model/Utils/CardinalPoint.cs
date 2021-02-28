using System;

namespace Delivery.Domain.Model.Utils
{
    public class CardinalPoint
    {
        public const char N = 'N';
        public const char E = 'E';
        public const char S = 'S';
        public const char W = 'W';

        public static readonly CardinalPoint NORTH = Create(N, W, E);
        public static readonly CardinalPoint EAST = Create(E, N, S);
        public static readonly CardinalPoint SOUTH = Create(S, E, W);
        public static readonly CardinalPoint WEST = Create(W, S, N);

        public char Value { get; private set; }
        public char Left { get; private set; }
        public char Right { get; private set; }

        public static CardinalPoint FromPoint(char p)
        {
            return p switch
            {
                N => NORTH,
                E => EAST,
                S => SOUTH,
                W => WEST,
                _ => throw new ArgumentException("Unknown cardinal point"),
            };
        }

        private static CardinalPoint Create(char p, char l, char r)
            => new CardinalPoint() { Value = p, Left = l, Right = r };
    }
}
