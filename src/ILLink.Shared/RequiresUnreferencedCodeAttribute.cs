
#if NETSTANDARD2_0
namespace System.Diagnostics.CodeAnalysis
{
	//
	// Summary:
	//     Indicates that the specified method requires dynamic access to code that is not
	//     referenced statically, for example, through System.Reflection.
	[AttributeUsage (AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	public sealed class RequiresUnreferencedCodeAttribute : Attribute
	{
		public string Message { get; }
		public string? Url { get; set; }

		public RequiresUnreferencedCodeAttribute (string message) { Message = message; }
	}
}
#endif