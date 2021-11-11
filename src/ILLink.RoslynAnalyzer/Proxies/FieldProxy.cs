
using Microsoft.CodeAnalysis;

namespace ILLink
{
    readonly struct FieldProxy
    {
        private readonly IFieldSymbol _field;
        public FieldProxy(IFieldSymbol field)
        {
            _field = field;
        }
    }
}