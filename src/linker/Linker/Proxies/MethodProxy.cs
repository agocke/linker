
using Mono.Cecil;
using Mono.Linker;

namespace ILLink
{
    readonly struct MethodProxy
    {
        private readonly MethodDefinition _method;

        public MethodProxy(MethodDefinition method)
        {
            _method = method;
        }

        public bool IsStaticConstructor() => _method.IsStaticConstructor();

        public bool IsStatic => _method.IsStatic;

        public bool IsConstructor => _method.IsConstructor;

        public static implicit operator EntityProxy(MethodProxy p) => new EntityProxy(p._method);

        public TypeProxy? DeclaringType => _method.DeclaringType is null
            ? null
            : new TypeProxy(_method.DeclaringType);
    }
}