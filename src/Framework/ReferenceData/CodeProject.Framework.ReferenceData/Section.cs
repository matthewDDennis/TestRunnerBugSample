using System;
using System.Collections.Generic;

namespace CodeProject.Framework.ReferenceData
{
    public interface ISection : IHasId
    {
        string Name { get; set; }
        bool Active { get; set; }
        string Description { get; set; }
        string Abstract { get; set; }
        string Directory { get; set; }
        string Advisory { get; set; }
        int SortOrder { get; set; }

        int ChapterId { get; set; }
        Chapter Chapter { get; set; }
        IList<Subsection> Subsections { get; }

        bool PaidContent { get; set; }
        bool AllowRating { get; set; }
        bool AllowUserContributions { get; set; }
        bool AllowContentFiltering { get; set; }

        // TODO: these should be in a separate table with a one to many relationship to Section
        DateTime? SponsorshipStartDate { get; set; }
        DateTime? SponsorshipEndDate { get; set; }
        string WallpaperUrl { get; set; }
        string WallpaperClickUrl { get; set; }
        string BackgroundColour { get; set; }
        bool UseSponsorHtmlBlurb { get; set; }
        string SponsorsHtmlBlurb { get; set; }
    }
    public class Section : ISection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public string Abstract { get; set; }
        public string Directory { get; set; }
        public string Advisory { get; set; }
        public int SortOrder { get; set; }

        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }
        public IList<Subsection> Subsections { get; } = new List<Subsection>();

        public bool PaidContent { get; set; }
        public bool AllowRating { get; set; }
        public bool AllowUserContributions { get; set; }
        public bool AllowContentFiltering { get; set; }

        // TODO: these should be in a separate table with a one to many relationship to Section
        public DateTime? SponsorshipStartDate { get; set; }
        public DateTime? SponsorshipEndDate { get; set; }
        public string WallpaperUrl { get; set; }
        public string WallpaperClickUrl { get; set; }
        public string BackgroundColour { get; set; }
        public bool UseSponsorHtmlBlurb { get; set; }
        public string SponsorsHtmlBlurb { get; set; }
    }

    public class SectionSummary : NameIdDescription
    {
        public SectionSummary(int id, string name, string description, string directory) : base(id, name, description)
        {
            Directory = directory;
        }

        public string Directory { get; }
    }

    public class SectionSummaryFactory : ISummaryFactory<ISection, SectionSummary>
    {
        public SectionSummary CreateSummary(ISection section)
        {
            if (section == null)
                return null;

            return new SectionSummary(section.Id, section.Name, section.Description, section.Directory);
        }
    }
}
