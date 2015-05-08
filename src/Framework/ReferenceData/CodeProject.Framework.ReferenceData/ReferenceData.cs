using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeProject.Framework.ReferenceData
{
    public class ReferenceData : IReferenceData
    {
        readonly IStaticDataQueryWithSummary<IPublishingStatus, PublishingStatusSummary> _publishingStatuses;
        readonly IStaticDataQueryWithSummary<ILicense, LicenseSummary>                   _licenses;
        readonly IStaticDataQueryWithSummary<ITag, TagSummary>                           _tags;
        readonly ITaxonomyQuery                                                          _taxonomy;

        public ReferenceData(IStaticDataQueryWithSummary<IPublishingStatus, PublishingStatusSummary> publishingStatuses,
                                    IStaticDataQueryWithSummary<ILicense,          LicenseSummary>          licenses,
                                    IStaticDataQueryWithSummary<ITag,              TagSummary>              tags,
                                    ITaxonomyQuery                                                          taxonomy)
        {
            _publishingStatuses = publishingStatuses;
            _licenses          = licenses;
            _tags              = tags;
            _taxonomy          = taxonomy;
        }

        public IStaticDataQueryWithSummary<IPublishingStatus, PublishingStatusSummary> PublishingStatuses { get { return _publishingStatuses; } }

        public IStaticDataQueryWithSummary<ILicense, LicenseSummary> Licenses { get { return _licenses; } }

        public IStaticDataQueryWithSummary<ITag, TagSummary> Tags { get { return _tags; } }

        public ITaxonomyQuery Taxonomy {  get { return _taxonomy; } }

    }
}
