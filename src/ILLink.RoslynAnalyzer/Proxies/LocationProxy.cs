
using Microsoft.CodeAnalysis;

namespace ILLink
{
    internal readonly struct LocationProxy
    {
        public readonly Location? Location;
        public LocationProxy(Location location)
        {
            Location = location;
        }
    }
}