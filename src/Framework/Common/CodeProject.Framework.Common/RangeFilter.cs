using System;

namespace CodeProject.Framework
{
    public enum ValueIs
    {
        BeforeRange = -1,
        InRange     = 0,
        AfterRange  = 1
    };

    public class RangeFilter<T>  where T : IComparable
    {
        /// <summary>
        /// The minimum value to include. Null means no minimum.
        /// </summary>
        public T Start { get; set; }

        /// <summary>
        /// If true then the range is > Start, else >= Start.  Default is false.
        /// </summary>
        public bool ExcludeStart { get; set; }

        /// <summary>
        /// The maximum value to include. Null means no maximum.
        /// </summary>
        public T End { get; set; }

        /// <summary>
        /// If true then the range is &lt; End, else &lt;= End.  Default is false.
        /// </summary>
        public bool ExcludeEnd { get; set; }

        /// <summary>
        /// Compares a value with the Range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>Whether the value is Before, In, or After the Range. </returns>
        ValueIs Compare(T value)
        {
            int startCheck = value.CompareTo(Start);
            if ((!(value is ValueType) && !Start.Equals(default(T))) &&
                (startCheck < 0 || (ExcludeStart && startCheck == 0)))
                return ValueIs.BeforeRange;

            int endCheck = value.CompareTo(End);
            if ((!(value is ValueType) && !End.Equals(default(T))) && 
                (endCheck > 0 || (ExcludeEnd && endCheck == 0)))
                return ValueIs.AfterRange;

            return ValueIs.InRange;
        }
    }


    public class NullableRangeFilter<T> where T : struct, IComparable
    {
        /// <summary>
        /// The minimum value to include. Null means no minimum.
        /// </summary>
        public T? Start { get; set; }

        /// <summary>
        /// If true then the range is > Start, else >= Start.  Default is false.
        /// </summary>
        public bool ExcludeStart { get; set; }

        /// <summary>
        /// The maximum value to include. Null means no maximum.
        /// </summary>
        public T? End { get; set; }

        /// <summary>
        /// If true then the range is &lt; End, else &lt;= End.  Default is false.
        /// </summary>
        public bool ExcludeEnd { get; set; }

        /// <summary>
        /// Compares a value with the Range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>Whether the value is Before, In, or After the Range. </returns>
        ValueIs Compare(T value)
        {
            if (Start.HasValue)
            {
                int startCheck = value.CompareTo(Start.Value);
                if (startCheck < 0 || (ExcludeStart && startCheck == 0))
                    return ValueIs.BeforeRange;
            }

            if (End.HasValue)
            {
                int endCheck = value.CompareTo(End.Value);
                if (endCheck > 0 || (ExcludeEnd && endCheck == 0))
                    return ValueIs.AfterRange;
            }

            return ValueIs.InRange;
        }
    }
}
