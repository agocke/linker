
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;

namespace ILLink
{
    readonly partial struct MethodProxy
    {
        private readonly IMethodSymbol _symbol;

        public MethodProxy(IMethodSymbol symbol)
        {
            _symbol = symbol;
        }

		public bool IsStaticConstructor () => _symbol.MethodKind == MethodKind.StaticConstructor;

        public bool IsStatic => _symbol.IsStatic;
        public bool IsConstructor => _symbol.MethodKind is MethodKind.Constructor or MethodKind.StaticConstructor;

        public TypeProxy? DeclaringType => new TypeProxy(_symbol.ContainingType);

        public static implicit operator EntityProxy(MethodProxy p) => new EntityProxy(p._symbol);
    }
}