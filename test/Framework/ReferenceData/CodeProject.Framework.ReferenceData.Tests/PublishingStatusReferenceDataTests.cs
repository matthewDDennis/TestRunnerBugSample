using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.DependencyInjection;
using CodeProject.Framework.ReferenceData.EF;
using Xunit;

namespace CodeProject.Framework.ReferenceData.Tests
{
    public class PublishingStatusReferenceDataTests : BaseTests
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
        public void CorrectCountsAreReturned(int numPubStatuses)
        {
            InitDatabase(numPubStatuses);

            CheckCounts(numPubStatuses);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(42)]
        public void LookingUpWithValidIdsReturnsValidData(int numPubStatuses)
        {
            InitDatabase(numPubStatuses);

            for (int i = 1; i <= numPubStatuses; i++)
            {
                var expected = CreatePublishingStatus(i);
                var PublishingStatus = _referenceData.PublishingStatuses.Get(i);

                Assert.NotNull(PublishingStatus);
                Assert.Equal(expected.Name, PublishingStatus.Name);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        public void LookingUpWithInLookingUpWithInvalidIdsReturnsNull(int numPubStatuses)
        {
            InitDatabase(numPubStatuses);

            // Chapters
            var PublishingStatus = _referenceData.PublishingStatuses.Get(-1);
            Assert.Null(PublishingStatus);

            PublishingStatus = _referenceData.PublishingStatuses.Get(0);
            Assert.Null(PublishingStatus);

            PublishingStatus = _referenceData.PublishingStatuses.Get(numPubStatuses + 1);
            Assert.Null(PublishingStatus);

            PublishingStatus = _referenceData.PublishingStatuses.Get(numPubStatuses + 2);
            Assert.Null(PublishingStatus);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(42)]
        public void PublishingStatusRefContainsCorrectData(int numPubStatuses)
        {
            InitDatabase(numPubStatuses);

            for (int i = 1; i <= numPubStatuses; i++)
            {
                PublishingStatus expected = CreatePublishingStatus(i);
                PublishingStatusSummary PublishingStatusRef = _referenceData.PublishingStatuses.GetSummary(i);

                Assert.NotNull(PublishingStatusRef);
                Assert.Equal(expected.Id, PublishingStatusRef.Id);
                Assert.Equal(expected.Name, PublishingStatusRef.Name);
                Assert.Equal(expected.Description, PublishingStatusRef.Description);
            }
        }

        private void InitDatabase(int numPubStatuses)
        {
            Assert.NotNull(_dbContext);

            for (int i = 1; i <= numPubStatuses; i++)
            {
                PublishingStatus newPublishingStatus = CreatePublishingStatus(i);
                _dbContext.PublishingStatuses.Add(newPublishingStatus);
            }

            _dbContext.SaveChanges();
        }

        private PublishingStatus CreatePublishingStatus(int i)
        {
            return new PublishingStatus
            {
                Id          = i,
                Name        = $"PublishingStatus{i}",
                Description = $"I am Publishing Status {i}"
            };
        }

        private void CheckCounts(int numPubStatuses)
        {
            var PublishingStatus = _referenceData.PublishingStatuses.GetAll();

            Assert.NotNull(PublishingStatus);
            Assert.Equal(numPubStatuses, PublishingStatus.Count());
        }

    }
}
