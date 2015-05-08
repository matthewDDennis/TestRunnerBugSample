using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeProject.Framework.ReferenceData
{
    public interface IPublishingStatus : IHasId
    {
        string Name { get; }
        string Description { get; }
        int SortOrder { get; }
    }

    public class PublishingStatus : IPublishingStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
    }

    public class PublishingStatusSummary : NameIdDescription
    {
        public PublishingStatusSummary(int id, string name, string description) : base(id, name, description)
        {
        }
    }

    public class PublishingStatusSummaryFactory : ISummaryFactory<IPublishingStatus, PublishingStatusSummary>
    {
        public PublishingStatusSummary CreateSummary(IPublishingStatus publishingStatus)
        {
            if (publishingStatus == null)
                return null;

            return new PublishingStatusSummary(publishingStatus.Id, publishingStatus.Name, publishingStatus.Description);
        }
    }
}
