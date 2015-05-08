using System;
using CodeProject.Framework.ReferenceData.EF;
using Microsoft.Framework.DependencyInjection;

namespace CodeProject.Framework.ReferenceData.Tests
{
    public class BaseTests : IDisposable
    {
        protected IServiceProvider           _serviceProvider;
        protected ContentStaticDataDbContext _dbContext;
        protected IReferenceData             _referenceData;

        public BaseTests()
        {
            var services = new ServiceCollection();
            services.AddEntityFramework();
            services.AddReferenceData().AddInMemory();

            _serviceProvider = services.BuildServiceProvider();
            _dbContext       = _serviceProvider.GetRequiredService<ContentStaticDataDbContext>();
            _referenceData   = _serviceProvider.GetRequiredService<IReferenceData>();
        }

        public void Dispose()
        {
           _dbContext.Dispose();
        }

    }
}
