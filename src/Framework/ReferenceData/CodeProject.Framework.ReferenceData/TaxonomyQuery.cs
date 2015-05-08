using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeProject.Framework.ReferenceData
{
    public class TaxonomyQuery : ITaxonomyQuery
    {
        private readonly ISummaryFactory<ISubsection, TaxonomySummary>  _taxonomySummaryFactory;
        private readonly Lazy<Dictionary<int,         TaxonomySummary>> _taxonomySummaries;

        public TaxonomyQuery(IStaticDataQueryWithSummary<IChapter,         ChapterSummary>    chapters,  
                             IStaticDataQueryWithSummary<ISection,         SectionSummary>    sections,   
                             IStaticDataQueryWithSummary<ISubsection,      SubsectionSummary> subSections,
                             ISummaryFactory<ISubsection, TaxonomySummary>   taxonomySummaryFactory)
        {
            Chapters                = chapters;
            Sections                = sections;
            Subsections             = subSections;
            _taxonomySummaryFactory = taxonomySummaryFactory;
            _taxonomySummaries      = new Lazy<Dictionary<int, TaxonomySummary>>(() => InitTaxonomy());

            // Force init
            _taxonomySummaries.Value.Count();
        }

        public IStaticDataQueryWithSummary<IChapter, ChapterSummary>       Chapters { get; }
        public IStaticDataQueryWithSummary<ISection, SectionSummary>       Sections { get; }
        public IStaticDataQueryWithSummary<ISubsection, SubsectionSummary> Subsections { get; }

        public TaxonomySummary GetSummary(int subsectionId)
        {
            TaxonomySummary summary;
            if (!_taxonomySummaries.Value.TryGetValue(subsectionId, out summary))
                summary = null;
            return summary;
        }

        /* ------------------------------------------------------------------------ */
        private Dictionary<int, TaxonomySummary> InitTaxonomy()
        {
            // get the dictionaries of Taxonomy information
            var summaryDictionary = new Dictionary<int, TaxonomySummary>();

            // Fixup the Section-Subsection relationships
            ILookup<int, ISubsection> sectionsSubSections = Subsections.GetAll().ToLookup(x => x.SectionId);
            foreach (IGrouping<int, ISubsection> sectionGrouping in sectionsSubSections)
            {
                Section section = Sections.Get(sectionGrouping.Key) as Section;
                if (section != null)
                {
                    foreach (Subsection subsection in sectionGrouping)
                    {
                        section.Subsections.Add(subsection);
                        subsection.Section = section;
                    }
                }
            }

            // Fixup the Chapter-Section relationships.
            ILookup<int, ISection> chapterSectionsLookup = Sections.GetAll().ToLookup(x => x.ChapterId);
            foreach (IGrouping<int, ISection> chapterSectionGrouping in chapterSectionsLookup)
            {
                Chapter chapter = Chapters.Get(chapterSectionGrouping.Key) as Chapter;
                if (chapter != null)
                {
                    foreach (Section section in chapterSectionGrouping)
                    {
                        chapter.Sections.Add(section);
                        section.Chapter = chapter;
                    }
                }
            }

            // Create dictionary of TaxonomySummaries
            foreach (Subsection subsection in Subsections.GetAll())
                summaryDictionary.Add(subsection.Id, _taxonomySummaryFactory.CreateSummary(subsection));

            return summaryDictionary;
        }
    }
}