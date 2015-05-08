using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.DependencyInjection;
using CodeProject.Framework.ReferenceData.EF;
using Xunit;

namespace CodeProject.Framework.ReferenceData.Tests
{
    public class TaxonomyReferenceTests : IDisposable
    {
        IServiceProvider         _serviceProvider;
        ContentStaticDataDbContext _dbContext;
        ITaxonomyQuery   _referenceData;

        public TaxonomyReferenceTests()
        {
            var services = new ServiceCollection();
            services.AddEntityFramework();
            services.AddReferenceData().AddInMemory();

            _serviceProvider = services.BuildServiceProvider();
            _dbContext       = _serviceProvider.GetRequiredService<ContentStaticDataDbContext>();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        [Fact]
        public void EmptyDatabaseReturnNoData()
        {
            InitDatabase(0, 0, 0);
            CheckCounts(0, 0, 0);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(10, 16, 22)]
        [InlineData(10, 20, 40)]
        public void CorrectCountsAreReturned(int numChapters, int numSections, int numSubsections)
        {
            InitDatabase(numChapters, numSections, numSubsections);
            _referenceData         = _serviceProvider.GetRequiredService<ITaxonomyQuery>();
            CheckCounts(numChapters, numSections, numSubsections);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 3)]
        [InlineData(10, 20, 40)]
        public void LookingUpWithValidIdsReturnsValidData(int numChapters, int numSections, int numSubsections)
        {
            InitDatabase(numChapters, numSections, numSubsections);
    
            for (int i = 1; i <= numChapters; i++)
            {
                var expected = CreateChapter(i);
                var chapter  = _referenceData.Chapters.Get(i);
                Assert.NotNull(chapter);
                Assert.Equal(expected.Name, chapter.Name);
            }

            for (int i = 1; i <= numSections; i++)
            {
                var expected = CreateSection(numChapters, i);
                var section  = _referenceData.Sections.Get(i);
                Assert.NotNull(section);
                Assert.Equal(expected.Name, section.Name);
            }

            for (int i = 1; i <= numSubsections; i++)
            {
                var expected    = CreateSubsection(numSections, i);
                var subsection  = _referenceData.Subsections.Get(i);
                var taxonomyRef = _referenceData.GetSummary(i);
                Assert.NotNull(subsection);
                Assert.Equal(expected.Name, subsection.Name);
                Assert.NotNull(taxonomyRef);
                Assert.Equal(expected.Name, taxonomyRef.Subsection.Name);
            }
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(0, 0, 0)]
        [InlineData(4, 5, 6)]
        [InlineData(10, 20, 40)]
        public void LookingUpWithInLookingUpWithInvalidIdsReturnsNull(int numChapters, int numSections, int numSubsections)
        {
            InitDatabase(numChapters, numSections, numSubsections);

            // Chapters
            var chapter = _referenceData.Chapters.Get(-1);
            Assert.Null(chapter);

            chapter = _referenceData.Chapters.Get(0);
            Assert.Null(chapter);

            chapter = _referenceData.Chapters.Get(numChapters+1);
            Assert.Null(chapter);

            chapter = _referenceData.Chapters.Get(numChapters+2);
            Assert.Null(chapter);

            // Sections
            var section = _referenceData.Sections.Get(-1);
            Assert.Null(section);

            section = _referenceData.Sections.Get(0);
            Assert.Null(section);

            section = _referenceData.Sections.Get(numSections + 1);
            Assert.Null(section);

            section = _referenceData.Sections.Get(numSections + 2);
            Assert.Null(section);

            var subsection = _referenceData.Subsections.Get(-1);
            Assert.Null(subsection);

            subsection = _referenceData.Subsections.Get(0);
            Assert.Null(subsection);

            // Subsections
            subsection = _referenceData.Subsections.Get(numSubsections + 1);
            Assert.Null(subsection);

            subsection = _referenceData.Subsections.Get(numSubsections + 2);
            Assert.Null(subsection);

        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(5, 8, 13)]
        [InlineData(21, 34, 55)]
        public void BuildsCorrectObjectMap(int numChapters, int numSections, int numSubsections)
        {
            InitDatabase(numChapters, numSections, numSubsections);

            // Verify top down
            foreach (var chapter in _referenceData.Chapters.GetAll())
            {
                foreach (var section in chapter.Sections)
                {
                    Assert.Equal(chapter.Id, section.ChapterId);
                    foreach (var subsection in section.Subsections)
                    {
                        Assert.Equal(section.Id, subsection.SectionId);
                    }
                }
            }

            // Verify bottom up
            foreach (var subsection in _referenceData.Subsections.GetAll())
            {
                var section = _referenceData.Sections.Get(subsection.SectionId);
                Assert.NotNull(section);
                Assert.True(section.Subsections.Contains(subsection));
            }

            foreach (var section in _referenceData.Sections.GetAll())
            {
                var chapter = _referenceData.Chapters.Get(section.ChapterId);
                Assert.NotNull(chapter);
                Assert.True(chapter.Sections.Contains(section));
            }
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(5, 8, 13)]
        [InlineData(21, 34, 55)]
        public void TaxonomyRefsContainCorrectData(int numChapters, int numSections, int numSubsections)
        {
            InitDatabase(numChapters, numSections, numSubsections);

            for (int i = 1; i <= numSubsections; i++)
            {
                var expectedSubsection = CreateSubsection(numSections, i);
                var expectedSection    = CreateSection(numChapters, expectedSubsection.SectionId);
                var expectedChapter    = CreateChapter(expectedSection.ChapterId);
                var result             = _referenceData.GetSummary(i);

                Assert.NotNull(result);
                Assert.Equal(expectedChapter.Id,          result.Chapter.Id);
                Assert.Equal(expectedChapter.Name,        result.Chapter.Name);
                Assert.Equal(expectedChapter.Description, result.Chapter.Description);

                Assert.Equal(expectedSection.Id,          result.Section.Id);
                Assert.Equal(expectedSection.Name,        result.Section.Name);
                Assert.Equal(expectedSection.Description, result.Section.Description);
                Assert.Equal(expectedSection.Directory,   result.Section.Directory);

                Assert.Equal(expectedSubsection.Id,          result.Subsection.Id);
                Assert.Equal(expectedSubsection.Name,        result.Subsection.Name);
                Assert.Equal(expectedSubsection.Description, result.Subsection.Description);
            }
        }

        private void InitDatabase(int numChapters, int numSections, int numSubsections)
        {
            Assert.NotNull(_dbContext);

            for (int i = 1; i <= numChapters; i++)
            {
                Chapter newChapter = CreateChapter(i);
                _dbContext.Chapters.Add(newChapter);
            }

            for (int i = 1; i <= numSections; i++)
            {
                Section newSection = CreateSection(numChapters, i);
                _dbContext.Sections.Add(newSection);
            }

            for (int i = 1; i <= numSubsections; i++)
            {
                Subsection newSubsection = CreateSubsection(numSections, i);
                _dbContext.Subsections.Add(newSubsection);
            }
            _dbContext.SaveChanges();
            _referenceData = _serviceProvider.GetRequiredService<ITaxonomyQuery>();
        }

        private static Subsection CreateSubsection(int numSections, int i)
        {
            return new Subsection
            {
                Id          = i,
                SectionId   = (numSections > 0) ? ((i - 1) % numSections) + 1 : 0,
                Name        = $"Subsection{i}",
                Description = $"I am Subsection {i}",
                SortOrder   = i,
                PaidContent = false
            };
        }

        private static Section CreateSection(int numChapters, int i)
        {
            return new Section
            {
                Id          = i,
                ChapterId   = (numChapters > 0) ? ((i - 1) % numChapters) + 1 : 0,
                Name        = $"Section{i}",
                Description = $"I am Section {i}",
                SortOrder   = i,
                Active      = true,
                Directory   = $"/Section{i}/",
                PaidContent = false
            };
        }

        private static Chapter CreateChapter(int i)
        {
            return new Chapter
            {
                Id          = i,
                Name        = $"Chapter{i}",
                Description = $"I am Chapter {i}",
                Graphic     = $"/images/Chapter{i}.png",
                SortOrder   = i
            };
        }

        private  void CheckCounts(int numChapters, int numSections, int numSubsections)
        {
            var chapters    = _referenceData.Chapters.GetAll();
            var sections    = _referenceData.Sections.GetAll();
            var subsections = _referenceData.Subsections.GetAll();

            Assert.NotNull(chapters);
            Assert.Equal(numChapters, chapters.Count());

            Assert.NotNull(sections);
            Assert.Equal(numSections, sections.Count());

            Assert.NotNull(subsections);
            Assert.Equal(numSubsections, subsections.Count());
        }
    }
}
