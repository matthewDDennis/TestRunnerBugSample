using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeProject.Framework
{
    public interface ISummaryFactory<T, TSummary>
    {
        TSummary CreateSummary(T entity);
    }
}
