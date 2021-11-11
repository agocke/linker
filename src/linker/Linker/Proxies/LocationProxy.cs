
using Mono.Cecil;
using Mono.Linker;

namespace ILLink
{
    readonly struct LocationProxy
    {
        public readonly MessageOrigin Origin;

        public LocationProxy(MessageOrigin origin)
        {
            Origin = origin;
        }

        public LocationProxy(IMemberDefinition member, int? ilOffset = null)
        {
			Origin = new MessageOrigin (member, ilOffset);
        }
    }
}