using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeProject.Framework
{

    public static class StaticDataQueryExtensions
    {
        public static IEnumerable<T> GetMany<T>(this IStaticDataQuery<T> lookup, IEnumerable<int> ids)
                    where T : class, IHasId
        {
            return ids?.Select(id => lookup.Get(id));
        }

        public static IEnumerable<T> GetMany<T>(this IStaticDataQuery<T> lookup, string commaIdList)
                    where T : class, IHasId
        {
            IEnumerable<int> ids = ConvertCommaDelimitedStringToInts(commaIdList);
            return lookup.GetMany(ids);
        }

        public static IEnumerable<TSummary> GetSummaries<T, TSummary>(this IStaticDataQueryWithSummary<T, TSummary> lookup, 
                                                                      IEnumerable<int> ids)
                    where T        : class, IHasId
                    where TSummary : class, IHasId
        {
            return ids?.Select(id => lookup.GetSummary(id));
        }

        public static IEnumerable<TSummary> GetSummaries<T, TSummary>(this IStaticDataQueryWithSummary<T, TSummary> lookup, string commaIdList)
                    where T        : class, IHasId
                    where TSummary : class, IHasId
        {
            IEnumerable<int> ids = ConvertCommaDelimitedStringToInts(commaIdList);
            return lookup.GetSummaries(ids);
        }

        /// <summary>
        /// Converts a comma delimited string of ints to an IEnumerable&lt;int>.
        /// </summary>
        /// <param name="commaIdList">The string</param>
        /// <returns>An IEnumerable of int, which can be empty. Null if a null string is passed in.</returns>
        private static IEnumerable<int> ConvertCommaDelimitedStringToInts(string commaIdList)
        {
            if (commaIdList == null)
                return null;

            IEnumerable<string> idStrings = commaIdList.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            IEnumerable<int> ids = idStrings.Select(s => { int i; return int.TryParse(s, out i) ? i : 0; });
            return ids;
        }
    }
}
