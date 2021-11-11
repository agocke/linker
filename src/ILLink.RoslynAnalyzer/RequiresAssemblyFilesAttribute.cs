
namespace ILLink
{
    public sealed class RequiresAssemblyFilesAttribute : RequiresCapabilityAttribute
    {
        public RequiresAssemblyFilesAttribute(string message, string? url)
            : base(message, url)
        { }
    }
}