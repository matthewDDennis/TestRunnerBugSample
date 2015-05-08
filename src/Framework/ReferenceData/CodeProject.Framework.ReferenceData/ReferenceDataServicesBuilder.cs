using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;

namespace CodeProject.Framework.ReferenceData
{
    public class ReferenceDataServicesBuilder : IAccessor<IServiceCollection>
    {
        private readonly IServiceCollection _services;

        public ReferenceDataServicesBuilder(IServiceCollection services)
        {
            _services = services;
        }

        IServiceCollection IAccessor<IServiceCollection>.Service => _services;
    }
}
