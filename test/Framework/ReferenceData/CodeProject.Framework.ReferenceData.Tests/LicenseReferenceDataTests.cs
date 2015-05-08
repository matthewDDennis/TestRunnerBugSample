using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.DependencyInjection;
using CodeProject.Framework.ReferenceData.EF;
using Xunit;

namespace CodeProject.Framework.ReferenceData.Tests
{
    public class LicenseReferenceDataTests : BaseTests
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
        public void CorrectCountsAreReturned(int numLicenses)
        {
            InitDatabase(numLicenses);

            CheckCounts(numLicenses);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(42)]
        public void LookingUpWithValidIdsReturnsValidData(int numLicenses)
        {
            InitDatabase(numLicenses);

            for (int i = 1; i <= numLicenses; i++)
            {
                var expected = CreateLicense(i);
                var License = _referenceData.Licenses.Get(i);

                Assert.NotNull(License);
                Assert.Equal(expected.Name, License.Name);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        public void LookingUpWithInLookingUpWithInvalidIdsReturnsNull(int numLicenses)
        {
            InitDatabase(numLicenses);

            // Chapters
            var License = _referenceData.Licenses.Get(-1);
            Assert.Null(License);

            License = _referenceData.Licenses.Get(0);
            Assert.Null(License);

            License = _referenceData.Licenses.Get(numLicenses + 1);
            Assert.Null(License);

            License = _referenceData.Licenses.Get(numLicenses + 2);
            Assert.Null(License);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(42)]
        public void LicenseRefContainsCorrectData(int numLicenses)
        {
            InitDatabase(numLicenses);

            for (int i = 1; i <= numLicenses; i++)
            {
                License expected = CreateLicense(i);
                LicenseSummary LicenseRef = _referenceData.Licenses.GetSummary(i);

                Assert.NotNull(LicenseRef);
                Assert.Equal(expected.Id, LicenseRef.Id);
                Assert.Equal(expected.Name, LicenseRef.Name);
                Assert.Equal(expected.Abbreviation, LicenseRef.Abbreviation);
                Assert.Equal(expected.Description, LicenseRef.Description);
                Assert.Equal(expected.Url, LicenseRef.Url);
            }
        }

        private void InitDatabase(int numLicenses)
        {
            Assert.NotNull(_dbContext);

            for (int i = 1; i <= numLicenses; i++)
            {
                License newLicense = CreateLicense(i);
                _dbContext.Licenses.Add(newLicense);
            }

            _dbContext.SaveChanges();
        }

        private License CreateLicense(int i)
        {
            return new License
            {
                Id           = i,
                Name         = $"License{i}",
                Abbreviation = $"Lic{i}",
                Description  = $"I am License {i}",
                Url          = $"/License/{i}"
            };
        }

        private void CheckCounts( int numLicenses)
        {
            var Licenses = _referenceData.Licenses.GetAll();

            Assert.NotNull(Licenses);
            Assert.Equal(numLicenses, Licenses.Count());
        }

    }
}
