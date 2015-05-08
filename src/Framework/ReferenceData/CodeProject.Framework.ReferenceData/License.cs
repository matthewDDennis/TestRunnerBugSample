using System;

namespace CodeProject.Framework.ReferenceData
{
    public interface ILicense : IHasId
    {
        string Name { get; }
        string Description { get; }
        string Url { get; }
        int SortOrder { get; }
        bool CopyrightProtection { get; }

        bool PatentLicense { get; } // misspelled in database

        bool Viral { get; }
        bool MustReleaseExtensions { get; }
        bool UseInClosedSource { get; }
        bool CommercialUse { get; }
        string ExampleUse { get; }
        string Abbreviation { get; }
        bool Supported { get; }
    }

    public class License : ILicense
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int SortOrder { get; set; }
        public bool CopyrightProtection { get; set; }

        public bool PatentLicense { get; set; } // misspelled in database

        public bool Viral { get; set; }
        public bool MustReleaseExtensions { get; set; }
        public bool UseInClosedSource { get; set; }
        public bool CommercialUse { get; set; }
        public string ExampleUse { get; set; }
        public string Abbreviation { get; set; }
        public bool Supported { get; set; }
    }

    public class LicenseSummary : NameIdDescription
    {
        public LicenseSummary(int id, string name, string abbreviation,
                              string description, string url )
            :base(id, name, description)
        {
            Abbreviation = abbreviation;
            Url          = url;
        }

        public string Abbreviation { get; }
        public string Url { get; }
    }

    public class LicenseSummaryFactory : ISummaryFactory<ILicense, LicenseSummary>
    {
        public LicenseSummary CreateSummary(ILicense license)
        {
            if (license == null)
                return null;

            return new LicenseSummary(license.Id, license.Name, license.Abbreviation,
                                      license.Description, license.Url);
        }
    }
}