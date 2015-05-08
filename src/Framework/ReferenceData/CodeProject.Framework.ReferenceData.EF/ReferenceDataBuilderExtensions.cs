using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.SqlServer.Extensions;
using Microsoft.Framework.DependencyInjection;

namespace CodeProject.Framework.ReferenceData.EF
{
    public static class ReferenceDataBuilderExtensions
    {
        public static ReferenceDataServicesBuilder AddSql(this ReferenceDataServicesBuilder builder, string connectionString)
        {
            var services = ((IAccessor<IServiceCollection>)builder).Service;
            var entityFrameworkServicesBuilder = new EntityFrameworkServicesBuilder(services);
            entityFrameworkServicesBuilder
                // .AddSqlServer()
                .AddDbContext<ContentStaticDataDbContext>(options =>
                    options.UseSqlServer(connectionString));

            AddLookupServices(services);

            return builder;
        }

        public static ReferenceDataServicesBuilder AddInMemory(this ReferenceDataServicesBuilder builder)
        {
            var services = ((IAccessor<IServiceCollection>)builder).Service;
            var entityFrameworkServicesBuilder = new EntityFrameworkServicesBuilder(services);
            entityFrameworkServicesBuilder
                .AddInMemoryStore()
                .AddDbContext<ContentStaticDataDbContext>(options =>
                        options.UseInMemoryStore());

            AddLookupServices(services);

            return builder;
        }

        private static void AddLookupServices(IServiceCollection services)
        {
            services.TryAdd(new ServiceCollection()
                .AddSingleton<IStaticDataQueryWithSummary<IPublishingStatus, PublishingStatusSummary>, 
                              StaticDataEF<ContentStaticDataDbContext, IPublishingStatus, 
                                           PublishingStatus, PublishingStatusSummary >>()

                              .AddSingleton<IStaticDataQueryWithSummary<ITag,TagSummary>,              
                                            StaticDataEF<ContentStaticDataDbContext, ITag, 
                                                         Tag, TagSummary>>()

                .AddSingleton<IStaticDataQueryWithSummary<ILicense, LicenseSummary>,          
                              StaticDataEF<ContentStaticDataDbContext, ILicense, 
                                           License, LicenseSummary>>()

                .AddSingleton<IStaticDataQueryWithSummary<IChapter, ChapterSummary>,         
                              StaticDataEF<ContentStaticDataDbContext, IChapter, 
                                           Chapter, ChapterSummary>>()

                .AddSingleton<IStaticDataQueryWithSummary<ISection, SectionSummary>,         
                              StaticDataEF<ContentStaticDataDbContext, ISection, 
                                           Section, SectionSummary>>()

                .AddSingleton<IStaticDataQueryWithSummary<ISubsection, SubsectionSummary>,
                              StaticDataEF<ContentStaticDataDbContext, ISubsection, 
                                           Subsection, SubsectionSummary >>()
            );
        }
    }
}
