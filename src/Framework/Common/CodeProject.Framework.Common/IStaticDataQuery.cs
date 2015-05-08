using System.Collections.Generic;
using System.Linq;

namespace CodeProject.Framework
{
    public interface IStaticDataQuery<T>
        where T : IHasId
    {
        IQueryable<T> GetAll();
        T Get(int id);
    }

    public interface IStaticDataQueryWithSummary<T, TSummary> : IStaticDataQuery<T>
        where T        : IHasId
        where TSummary : IHasId
    {
        IQueryable<TSummary> GetAllSummaries();
        TSummary GetSummary(int id);

    }
}