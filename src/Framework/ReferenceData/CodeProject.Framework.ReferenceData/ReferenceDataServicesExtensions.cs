using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.DependencyInjection;

namespace CodeProject.Framework.ReferenceData
{
    public static class ReferenceDataServicesExtensions
    {
        public static ReferenceDataServicesBuilder AddReferenceData(this IServiceCollection services)
        {
            services.TryAdd(new ServiceCollection()
                .AddSingleton<ISummaryFactory<IChapter,          ChapterSummary>,          ChapterSummaryFactory>()
                .AddSingleton<ISummaryFactory<ISection,          SectionSummary>,          SectionSummaryFactory>()
                .AddSingleton<ISummaryFactory<ISubsection,       SubsectionSummary>,       SubsectionSummaryFactory>()
                .AddSingleton<ISummaryFactory<ILicense,          LicenseSummary>,          LicenseSummaryFactory>()
                .AddSingleton<ISummaryFactory<IPublishingStatus, PublishingStatusSummary>, PublishingStatusSummaryFactory>()
                .AddSingleton<ISummaryFactory<ITag,              TagSummary>,              TagSummaryFactory>()
                .AddSingleton<ISummaryFactory<ISubsection,       TaxonomySummary>,         TaxonomySummaryFactory>()

                .AddSingleton<ITaxonomyQuery, TaxonomyQuery>()
                .AddSingleton<IReferenceData, ReferenceData>()
            );

            return new ReferenceDataServicesBuilder(services);
        }
    }
}
