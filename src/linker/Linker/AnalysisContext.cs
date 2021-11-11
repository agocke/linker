
using System;
using System.Collections.Generic;
using ILLink.Shared;
using Mono.Linker;

namespace ILLink
{
    internal partial struct AnalysisContext
    {
        private readonly LinkContext _linkContext;

        public AnalysisContext(LinkContext linkContext)
        {
            _linkContext = linkContext;
        }

		public IEnumerable<T> GetLinkerAttributes<T> (EntityProxy entity) where T : Attribute
            => _linkContext.Annotations.GetLinkerAttributes<T>(entity.Member);

		public void ReportDiagnostic(DiagnosticId diagId, LocationProxy locationProxy, params string[] args)
        {
			_linkContext.LogWarning (
                new DiagnosticString (DiagnosticId.RequiresUnreferencedCodeOnStaticConstructor).GetMessage (args),
                (int) diagId,
                locationProxy.Origin,
                MessageSubCategory.TrimAnalysis);
        }
    }
}