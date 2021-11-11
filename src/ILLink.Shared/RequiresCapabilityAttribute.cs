
using System;
using ILLink.Shared;

namespace ILLink
{
    /// <summary>
    /// Represents an instance of a "Requires..." attribute, like <see cref="RequiresUnreferencedCodeAttribute" />.
    /// </summary>
    public abstract class RequiresCapabilityAttribute : Attribute
    {
        public readonly string Message;
        public readonly string? Url;
        public RequiresCapabilityAttribute(string message, string? url)
        {
            Message = message;
            Url = url;
        }

        public string GetFormattedMessage() => MessageFormat.FormatRequiresAttributeMessageArg(Message);

        public string GetFormattedUrl() => MessageFormat.FormatRequiresAttributeUrlArg(Url);
    }

    public sealed class RequiresUnreferencedCodeAttribute : RequiresCapabilityAttribute
    {
        public RequiresUnreferencedCodeAttribute(string message, string? url)
            : base(message, url)
        { }
    }
}