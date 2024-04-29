using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.PagedList
{
    public static class PagedObjectExtensions
    {
        /// <summary>
        /// Creates a subset of this collection of objects as items in an object containing
        /// metadata about the collection of objects the subset was created from.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>
        /// <param name="superset">
        /// The collection of objects to be divided into subsets. If the collection
        /// implements <see cref="IEnumerable{T}"/>, it will be treated as such.
        /// </param>
        /// <param name="pageNumber">
        /// The one-based index of the subset of objects to be contained by this instance.
        /// </param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <returns>A subset of this collection of objects that can be individually accessed by index and containing
        /// metadata about the collection of objects the subset was created from.</returns>
        public static PagedObject<T> ToPagedObject<T>(this IEnumerable<T> superset, int pageNumber, int pageSize)
        {
            return new PagedObject<T>(superset ?? new List<T>(), pageNumber, pageSize);
        }

        /// <summary>
        /// Creates a subset of this collection of objects as items in an object containing
        /// metadata about the collection of objects the subset was created from.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>
        /// <param name="superset">
        /// The collection of objects to be divided into subsets. If the collection
        /// implements <see cref="IQueryable{T}"/>, it will be treated as such.
        /// </param>
        /// <param name="pageNumber">
        /// The one-based index of the subset of objects to be contained by this instance.
        /// </param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <param name="totalSetCount">The total size of set</param>
        /// <returns>A subset of this collection of objects that can be individually accessed by index and containing
        /// metadata about the collection of objects the subset was created from.</returns>
        public static PagedObject<T> ToPagedObject<T>(this IQueryable<T> superset, int pageNumber, int pageSize)
        {
            if (superset == null)
            {
                throw new ArgumentNullException(nameof(superset));
            }

            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException($"pageNumber = {pageNumber}. PageNumber cannot be below 1.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException($"pageSize = {pageSize}. PageSize cannot be less than 1.");
            }

            return new PagedObject<T>(superset, pageNumber, pageSize);
        }

        public static Task<PagedObject<T>> ToPagedObjectAsync<T>(this IEnumerable<T> superset, int pageNumber, int pageSize)
        {
            return Task.Factory.StartNew(() =>
            {
                return new PagedObject<T>(superset ?? new List<T>(), pageNumber, pageSize);
            });
        }

        public static Task<PagedObject<T>> ToPagedObjectAsync<T>(this IQueryable<T> superset, int pageNumber, int pageSize)
        {
            return Task.Factory.StartNew(() =>
            {
                return new PagedObject<T>(superset, pageNumber, pageSize);
            });
        }
    }
}