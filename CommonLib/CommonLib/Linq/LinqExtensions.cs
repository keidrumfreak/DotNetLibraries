using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Linq
{
    public static class LinqExtensions
    {
        sealed class CommonSelector<T, TKey> : IEqualityComparer<T>
        {
            readonly Func<T, TKey> selector;

            public CommonSelector(Func<T,TKey> selector)
            {
                this.selector = selector;
            }

            public bool Equals(T x, T y)
            {
                return selector(x).Equals(selector(y));
            }

            public int GetHashCode(T obj)
            {
                return selector(obj).GetHashCode();
            }
        }

        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            return source.Distinct(new CommonSelector<T, TKey>(selector));
        }
    }
}
