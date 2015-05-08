using System;

namespace CodeProject.Framework.ReferenceData
{
    public interface ISubsection : IHasId
    {
        string Name { get; }
        string Description { get; }
        string Explanation { get; }
        string Keywords { get; }
        string Advisory { get; }
        int SortOrder { get; }

        int SectionId { get; }
        Section Section { get; }

        bool PaidContent { get; }
        bool AllowRating { get; }
        bool AllowUserContributions { get; }
        bool IncludeInSummaries { get; }
    }

    public class Subsection : ISubsection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Explanation { get; set; }
        public string Keywords { get; set; }
        public string Advisory { get; set; }
        public int SortOrder { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }

        public bool PaidContent { get; set; }
        public bool AllowRating { get; set; }
        public bool AllowUserContributions { get; set; }
        public bool IncludeInSummaries { get; set; }
    }

    public class SubsectionSummary : NameIdDescription
    {
        public SubsectionSummary(int id, string name, string description) : base(id, name, description)
        {
        }
    }

    public class SubsectionSummaryFactory : ISummaryFactory<ISubsection, SubsectionSummary>
    {
        public SubsectionSummary CreateSummary(ISubsection subsection)
        {
            if (subsection == null)
                return null;

            return new SubsectionSummary(subsection.Id, subsection.Name, subsection.Description);
        }
    }
}
