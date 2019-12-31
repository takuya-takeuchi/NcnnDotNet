using System.Collections.Generic;

namespace NcnnDotNet.Extensions
{

    public static class EnumerableExtensions
    {

        internal static void ThrowIfDisposed<T>(this IEnumerable<T> items)
            where T : NcnnObject
        {
            foreach (var item in items)
                item.ThrowIfDisposed();
        }

        internal static void ThrowIfDisposed<T>(this IEnumerable<IEnumerable<T>> items)
            where T : NcnnObject
        {
            foreach (var elements in items)
                foreach (var item in elements)
                    item.ThrowIfDisposed();
        }

        internal static void ThrowIfDisposed<T>(this IEnumerable<IList<T>> items)
            where T : NcnnObject
        {
            foreach (var elements in items)
            foreach (var item in elements)
                item.ThrowIfDisposed();
        }

        public static void DisposeElement<T>(this IEnumerable<T> items)
            where T : NcnnObject
        {
            foreach (var item in items)
                item?.Dispose();
        }

        public static void DisposeElement<T>(this IEnumerable<IEnumerable<T>> items)
            where T : NcnnObject
        {
            foreach (var elements in items)
                foreach (var item in elements)
                    item.Dispose();
        }

    }

}
