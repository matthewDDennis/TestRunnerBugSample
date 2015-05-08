using System;
using System.Collections.Generic;

namespace CodeProject.Framework.ReferenceData
{
    public interface IChapter : IHasId
    {
        string Name { get; }
        string Description { get; }
        string Graphic { get; }
        int SortOrder { get; }

        IList<Section> Sections { get; }

        // TODO: these should be in a separate table with a one to many relationship to Chapter
        DateTime? SponsorshipStartDate { get; }
        DateTime? SponsorshipEndDate { get; }
        string WallpaperUrl { get; }
        string WallpaperClickUrl { get; }
        string BackgroundColour { get; }
        bool UseSponsorHtmlBlurb { get; }
        string SponsorsHtmlBlurb { get; }
    }

    public class Chapter : IChapter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Graphic { get; set; }
        public int SortOrder { get; set; }

        public IList<Section> Sections { get; } = new List<Section>();

        // TODO: these should be in a separate table with a one to many relationship to Chapter
        public DateTime? SponsorshipStartDate { get; set; }
        public DateTime? SponsorshipEndDate { get; set; }
        public string WallpaperUrl { get; set; }
        public string WallpaperClickUrl { get; set; }
        public string BackgroundColour { get; set; }
        public bool UseSponsorHtmlBlurb { get; set; }
        public string SponsorsHtmlBlurb { get; set; }
    }

    public class ChapterSummary : NameIdDescription
    {
        public ChapterSummary(int id, string name, string description) : base(id, name, description)
        {
        }
    }

    public class ChapterSummaryFactory : ISummaryFactory<IChapter, ChapterSummary>
    {
        public ChapterSummary CreateSummary(IChapter chapter)
        {
            if (chapter == null)
                return null;

            return new ChapterSummary(chapter.Id, chapter.Name, chapter.Description);
        }
    }
}
