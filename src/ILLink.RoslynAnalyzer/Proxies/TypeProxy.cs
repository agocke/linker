
using Microsoft.CodeAnalysis;

namespace ILLink
{
    readonly struct TypeProxy
    {
        private readonly ITypeSymbol _symbol;
        public TypeProxy(ITypeSymbol symbol)
        {
            _symbol = symbol;
        }

        public static implicit operator EntityProxy(TypeProxy p) => new EntityProxy(p._symbol);
    }
}