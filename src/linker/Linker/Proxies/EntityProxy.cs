
using Mono.Cecil;
using Mono.Linker;

namespace ILLink
{
    readonly struct EntityProxy
    {
        public readonly IMemberDefinition Member;

        public EntityProxy(IMemberDefinition member)
        {
            Member = member;
        }

        public LocationProxy GetLocation()
        {
            return new LocationProxy(Member);
        }

        public string ToDisplayString()
        {
            return Member is MemberReference memberRef ? memberRef.GetDisplayName () : Member.FullName;
        }

        public bool IsStatic => Member switch
        {
            EventDefinition { AddMethod: {} m } => m.IsStatic,
            FieldDefinition f => f.IsStatic,
            MethodDefinition m => m.IsStatic,
            PropertyDefinition p => !p.HasThis,
            TypeDefinition t => t.IsAbstract && t.IsSealed,
            _ => throw new InternalErrorException("Location thought to be unreachable")
        };

        public TypeProxy DeclaringType => new TypeProxy(Member.DeclaringType);

        public bool IsStaticConstructor() => (Member as MethodDefinition)?.IsStaticConstructor() == true;

        public bool IsConstructor => (Member as MethodDefinition)?.IsConstructor == true;

        public bool Is(out MethodProxy methodProxy)
        {
            if (Member is MethodDefinition m) {
                methodProxy = new MethodProxy(m);
                return true;
            }
            methodProxy = default;
            return false;
        }
    }
}