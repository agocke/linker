using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ILLink.Shared;

namespace ILLink
{
    internal partial struct AnalysisContext
    {
		public bool TryGetLinkerAttribute<T> (EntityProxy member, [NotNullWhen (returnValue: true)] out T? attribute) where T : Attribute
		{
			var attributes = GetLinkerAttributes<T> (member);
			if (attributes.Count () > 1) {
				ReportDiagnostic (
					DiagnosticId.DuplicateLinkerAttributes,
					member.GetLocation(),
					member.ToDisplayString());
			}

			attribute = attributes.FirstOrDefault ();
			return attribute != null;
		}

		/// <summary>
		/// Determines if method requires unreferenced code (and thus any usage of such method should be warned about).
		/// </summary>
		/// <remarks>Unlike <see cref="IsMethodInRequiresUnreferencedCodeScope(MethodDefinition)"/> only static methods
		/// and .ctors are reported as requiring unreferenced code when the declaring type has RUC on it.</remarks>
		internal bool DoesMethodRequireCapability<T> (MethodProxy method, [NotNullWhen (returnValue: true)] out T? attribute)
			where T : RequiresCapabilityAttribute
			=> DoesTargetRequireCapability(method, out attribute);

		internal bool DoesMethodRequireUnreferencedCode (
			MethodProxy method,
			[NotNullWhen (returnValue: true)] out RequiresUnreferencedCodeAttribute? attr)
			=> DoesMethodRequireCapability (method, out attr);

		internal bool DoesTargetRequireCapability<T> (
			EntityProxy target,
			[NotNullWhen (returnValue: true)] out T? attribute)
			where T : RequiresCapabilityAttribute
		{
			if (target.IsStaticConstructor ()) {
				attribute = null;
				return false;
			}

			if (TryGetLinkerAttribute (target, out attribute))
				return true;

			if ((target.IsStatic || target.IsConstructor) && target.DeclaringType is {} declType &&
				TryGetLinkerAttribute (declType, out attribute))
				return true;

			return false;
		}
    }
}