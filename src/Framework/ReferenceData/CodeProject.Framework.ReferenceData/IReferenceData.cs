using System;
using System.Collections.Generic;

namespace CodeProject.Framework.ReferenceData
{
    // TODO: Some of this should go to a ArticleReferenceData class in the Article Module.
    public interface IReferenceData
    {
        IStaticDataQueryWithSummary<IPublishingStatus, PublishingStatusSummary> PublishingStatuses { get; }
        IStaticDataQueryWithSummary<ILicense, LicenseSummary>                   Licenses  { get; }
        IStaticDataQueryWithSummary<ITag, TagSummary>                           Tags  { get; }

        ITaxonomyQuery                                                    Taxonomy  { get; }
    }
}