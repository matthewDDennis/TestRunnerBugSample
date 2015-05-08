using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.DependencyInjection;
using CodeProject.Framework.ReferenceData.EF;
using Xunit;

namespace CodeProject.Framework.ReferenceData.Tests
{
    public class TagReferenceDataTests : BaseTests
    {
        [Fact]
        public void EmptyDatabaseReturnNoData()
        {
            CheckCounts(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(42)]
        public void CorrectCountsAreReturned(int numTags)
        {
            InitDatabase(numTags);

            CheckCounts(numTags);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(42)]
        public void LookingUpWithValidIdsReturnsValidData(int numTags)
        {
            InitDatabase(numTags);

            for (int i = 1; i <= numTags; i++)
            {
                var expected = CreateTag(i);
                var Tag = _referenceData.Tags.Get(i);

                Assert.NotNull(Tag);
                Assert.Equal(expected.Name, Tag.Name);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        public void LookingUpWithInLookingUpWithInvalidIdsReturnsNull(int numTags)
        {
            InitDatabase(numTags);

            // Chapters
            var Tag = _referenceData.Tags.Get(-1);
            Assert.Null(Tag);

            Tag = _referenceData.Tags.Get(0);
            Assert.Null(Tag);

            Tag = _referenceData.Tags.Get(numTags + 1);
            Assert.Null(Tag);

            Tag = _referenceData.Tags.Get(numTags + 2);
            Assert.Null(Tag);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(42)]
        public void TagRefContainsCorrectData(int numTags)
        {
            InitDatabase(numTags);

            for (int i = 1; i <= numTags; i++)
            {
                Tag expected = CreateTag(i);
                TagSummary TagRef = _referenceData.Tags.GetSummary(i);

                Assert.NotNull(TagRef);
                Assert.Equal(expected.Id, TagRef.Id);
                Assert.Equal(expected.Name, TagRef.Name);
                Assert.Equal(expected.Count, TagRef.Count);
            }
        }

        private void InitDatabase(int numTags)
        {
            Assert.NotNull(_dbContext);

            for (int i = 1; i <= numTags; i++)
            {
                Tag newTag = CreateTag(i);
                _dbContext.Tags.Add(newTag);
            }

            _dbContext.SaveChanges();
        }

        private Tag CreateTag(int i)
        {
            return new Tag
            {
                Id = i,
                Name = $"Tag{i}",
                Count = (i * 123) % 47
            };
        }

        private void CheckCounts(int numTags)
        {
            var Tags = _referenceData.Tags.GetAll();

            Assert.NotNull(Tags);
            Assert.Equal(numTags, Tags.Count());
        }

    }
}
