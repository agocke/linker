
using Mono.Cecil;

namespace ILLink
{
    readonly struct TypeProxy
    {
        private readonly TypeDefinition _typeDef;

        public TypeProxy(TypeDefinition typeDef)
        {
            _typeDef = typeDef;
        }

        public static implicit operator EntityProxy(TypeProxy p) => new EntityProxy(p._typeDef);
    }
}