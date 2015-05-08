using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace CodeProject.Framework.ReferenceData.EF
{
    public class StaticDataEF<TDbContext, TReadOnly, T, TSummary> : IStaticDataQueryWithSummary<TReadOnly, TSummary>
        where TDbContext : DbContext
        where TReadOnly : class, IHasId
        where T         : class, TReadOnly
        where TSummary  : class, IHasId
    {
        class ThingCollections
        {
            public Dictionary<int, TReadOnly> ThingDictionary { get; set; }
            public List<TReadOnly>            ThingList { get; set; }
            public Dictionary<int, TSummary>  ThingSummaryDictionary { get; set; }
            public List<TSummary>             ThingSummaryList { get; set; }
        }

        private readonly Lazy<ThingCollections>               _thingCollections;
        private readonly TDbContext                           _dbContext;
        private readonly ISummaryFactory<TReadOnly, TSummary> _summaryFactory;

        public StaticDataEF(TDbContext dbContext, ISummaryFactory<TReadOnly, TSummary> summaryFactory)
        {
            _dbContext      = dbContext;
            _summaryFactory = summaryFactory;
            _thingCollections        = new Lazy<ThingCollections>(Initialize);
        }

        public IQueryable<TReadOnly> GetAll()
        {
            return _thingCollections.Value.ThingList.AsQueryable();
        }

        public TReadOnly Get(int id)
        {
            TReadOnly thing;
            if (!_thingCollections.Value.ThingDictionary.TryGetValue(id, out thing))
                thing = null;
            return thing;
        }

        public IQueryable<TSummary> GetAllSummaries()
        {
            return _thingCollections.Value.ThingSummaryList.AsQueryable();
        }

        public TSummary GetSummary(int id)
        {
            TSummary summary;
            if (!_thingCollections.Value.ThingSummaryDictionary.TryGetValue(id, out summary))
                summary = null;

            return summary;
        }

        private ThingCollections Initialize()
        {
            ThingCollections thingCollections       = new ThingCollections();
                                                    
            thingCollections.ThingList              = _dbContext.Set<T>()
                                                      .AsNoTracking()
                                                      .Select(x => x as TReadOnly)
                                                      .ToList();
                                                    
            thingCollections.ThingDictionary        = thingCollections.ThingList
                                                      .ToDictionary(x => x.Id);
                                                    
            thingCollections.ThingSummaryList       = thingCollections.ThingList
                                                      .Select(thing => _summaryFactory.CreateSummary(thing))
                                                      .ToList();
                                                    
            thingCollections.ThingSummaryDictionary = thingCollections.ThingSummaryList
                                                      .ToDictionary(x => x.Id);

            return thingCollections;
        }
    }
}