using System;
using System.Collections.Generic;

namespace Delivery.Domain.Model
{
    public interface IRoute
    {
        IEnumerable<ICommand> Commands { get; }
    }

    public class Route : IRoute
    {
        public IEnumerable<ICommand> Commands { get; private set; }

        public static IRoute FromString(string route)
        {
            if (string.IsNullOrEmpty(route))
                throw new ArgumentException("Invalid routes");

            return Create(route);
        }

        private static IRoute Create(string value) => new Route()
        {
            Commands = CommandFactory.FromString(value)
        };
    }
}
