using System;

namespace CodeProject.Framework.ReferenceData
{
    public class TaxonomySummary : IHasId
    {
        public int               Id => Subsection.Id;
        public ChapterSummary    Chapter { get; }
        public SectionSummary    Section { get; }
        public SubsectionSummary Subsection { get; }

        public TaxonomySummary(ChapterSummary chapter, SectionSummary section, SubsectionSummary subsection)
        {
            Chapter    = chapter;
            Section    = section;
            Subsection = subsection;
        }
    }

    public class TaxonomySummaryFactory : ISummaryFactory<ISubsection, TaxonomySummary>
    {
        private readonly ISummaryFactory<IChapter, ChapterSummary>       _chapterSummaryFactory;
        private readonly ISummaryFactory<ISection, SectionSummary>       _sectionSummaryFactory;
        private readonly ISummaryFactory<ISubsection, SubsectionSummary> _subsectionSummaryFactory;

        public TaxonomySummaryFactory(ISummaryFactory<IChapter, ChapterSummary>       chapterSummaryFactory,
                                      ISummaryFactory<ISection, SectionSummary>       sectionSummaryFactory,
                                      ISummaryFactory<ISubsection, SubsectionSummary> subsectionSummaryFactory)
        {
            _chapterSummaryFactory    = chapterSummaryFactory;
            _sectionSummaryFactory    = sectionSummaryFactory;
            _subsectionSummaryFactory = subsectionSummaryFactory;
        }

        public TaxonomySummary CreateSummary(ISubsection subsection)
        {
            if (subsection == null)
                return null;

            return new TaxonomySummary(_chapterSummaryFactory.CreateSummary(subsection?.Section?.Chapter),
                                       _sectionSummaryFactory.CreateSummary(subsection?.Section),
                                       _subsectionSummaryFactory.CreateSummary(subsection));
        }
    }
}