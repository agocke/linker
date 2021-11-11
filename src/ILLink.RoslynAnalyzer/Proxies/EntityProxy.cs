
using System;
using System.Collections.Immutable;
using ILLink.RoslynAnalyzer;
using Microsoft.CodeAnalysis;

namespace ILLink
{
    /// <summary>
    /// Represents any entity in the CLR type system.
    /// </summary>
    internal readonly record struct EntityProxy(ISymbol Symbol) : IEquatable<EntityProxy>
    {
        public ImmutableArray<AttributeData> GetAttributes() => Symbol.GetAttributes();

        public LocationProxy GetLocation()
        {
            if (Symbol.Locations.Length == 0)
            {
                return default;
            }
            return new LocationProxy(Symbol.Locations[0]);
        }

        public string ToDisplayString() => Symbol.ToDisplayString();

        public bool IsStatic => Symbol.IsStatic;

        public TypeProxy DeclaringType => new TypeProxy(Symbol.ContainingType);

        public bool IsStaticConstructor() => Symbol.IsStaticConstructor();

        public bool IsConstructor => Symbol.IsConstructor();
	}
}