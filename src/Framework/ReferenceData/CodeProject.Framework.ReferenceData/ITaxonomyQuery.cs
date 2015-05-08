using System.Collections.Generic;

namespace CodeProject.Framework.ReferenceData
{
    public interface ITaxonomyQuery
    {
        IStaticDataQueryWithSummary<IChapter,    ChapterSummary>    Chapters { get; }
        IStaticDataQueryWithSummary<ISection,    SectionSummary>    Sections { get; }
        IStaticDataQueryWithSummary<ISubsection, SubsectionSummary> Subsections { get; }
        TaxonomySummary GetSummary(int subsectionId);
    }
}