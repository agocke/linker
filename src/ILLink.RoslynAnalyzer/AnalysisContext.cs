
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ILLink.RoslynAnalyzer;
using ILLink.Shared;
using Microsoft.CodeAnalysis;

#nullable enable

namespace ILLink
{
    internal partial struct AnalysisContext
    {
		const string RucName = RequiresUnreferencedCodeAnalyzer.FullyQualifiedRequiresUnreferencedCodeAttribute;
		const string RafName = RequiresAssemblyFilesAnalyzer.RequiresAssemblyFilesAttributeFullyQualifiedName;

        private readonly Action<Diagnostic> _reportDiagnostic;

        public AnalysisContext(Action<Diagnostic> reportDiagnostic)
        {
            _reportDiagnostic = reportDiagnostic;
        }

		public IEnumerable<T> GetLinkerAttributes<T> (EntityProxy member) where T : Attribute
		{

            var attrs = member.GetAttributes();
			if (attrs.IsEmpty)
				return Enumerable.Empty<T> ();

			var ctx = this;
            return Enumerate();

            IEnumerable<T> Enumerate()
			{
				foreach (var ca in member.GetAttributes ()) {
					var attrClass = ca.AttributeClass;
					if (attrClass is null) {
						continue;
					}
					if (typeof(T) == typeof(ILLink.RequiresUnreferencedCodeAttribute) &&
						attrClass.HasName(RucName) == true) {
						yield return (T)  ctx.ProcessRuc (ca, member.GetLocation());
					}
					else if (typeof(T) == typeof(ILLink.RequiresAssemblyFilesAttribute) &&
						attrClass.HasName(RafName) == true) {
						yield return (T)  ctx.ProcessRaf (ca, member.GetLocation());
					}
				}
			}
		}

		public void ReportDiagnostic(DiagnosticId diagId, LocationProxy locationProxy, params object[] args)
        {
            var location = locationProxy.Location;
            if (location?.IsInSource != false) {
				_reportDiagnostic (Diagnostic.Create (
					DiagnosticDescriptors.GetDiagnosticDescriptor (diagId),
					location,
					args));
			}
        }

		Attribute ProcessRuc (AttributeData ca, LocationProxy location)
		{
			string message = "";
			string? url = null;

			if (ca.ConstructorArguments is { Length: 1 } args &&
				args[0] is { Type: { SpecialType: SpecialType.System_String }, Value: string arg }) {
                message = arg;
			}
			else {
				ReportDiagnostic (
					DiagnosticId.AttributeDefinitionMissingParameters,
					location,
					RucName);
			}
			foreach (var namedArg in ca.NamedArguments) {
				if (namedArg.Key == "Url") {
					url = namedArg.Value.Value as string;
					break;
				}
			}

			return new RequiresUnreferencedCodeAttribute(message, url);
		}

		Attribute ProcessRaf (AttributeData ca, LocationProxy location)
		{
			string message = "";
			string? url = null;
			if (ca.ConstructorArguments is { Length: 1 } args &&
				args[0] is { Type: { SpecialType: SpecialType.System_String }, Value: string arg }) {
				message = arg;
			}

			foreach (var namedArg in ca.NamedArguments) {
				if (namedArg.Key == "Url") {
					url = namedArg.Value.Value as string;
					break;
				}
			}

			return new RequiresAssemblyFilesAttribute (message, url);
		}

    }
}