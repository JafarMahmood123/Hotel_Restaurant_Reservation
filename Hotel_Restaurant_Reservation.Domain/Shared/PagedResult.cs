using System;
using System.Collections.Generic;

namespace Hotel_Restaurant_Reservation.Domain.Shared
{
    /// <summary>
    /// Represents a paginated list of items.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    public class PagedResult<T>
    {
        public PagedResult(IReadOnlyList<T> items, int page, int pageSize, int totalCount)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        /// <summary>
        /// The items for the current page.
        /// </summary>
        public IReadOnlyList<T> Items { get; }

        /// <summary>
        /// The current page number.
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// The number of items per page.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// The total number of items across all pages.
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// The total number of pages.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        /// <summary>
        /// A flag indicating if there is a next page.
        /// </summary>
        public bool HasNextPage => Page < TotalPages;

        /// <summary>
        /// A flag indicating if there is a previous page.
        /// </summary>
        public bool HasPreviousPage => Page > 1;

        /// <summary>
        /// Creates an empty PagedResult.
        /// </summary>
        public static PagedResult<T> Empty(int page, int pageSize) => new(new List<T>(), page, pageSize, 0);
    }
}
