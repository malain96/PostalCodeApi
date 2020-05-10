using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PostalCodeApi.Extensions
{
    public class PagedAndSortedList<T> : List<T>
    {
        public PagedAndSortedList(List<T> items, int count, int pageNumber, int pageSize, string sorted,
            string sortedBy)
        {
            TotalCount = count;
            PageSize = pageSize;
            Sorted = sorted;
            SortedBy = sortedBy;
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            Items = items;
        }

        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public string Sorted { get; }
        public string SortedBy { get; }

        public List<T> Items { get; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public static async Task<PagedAndSortedList<T>> ToPagedListAsync(IQueryable<T> source, int pageNumber,
            int pageSize, string sorted, string sortedBy)
        {
            var count = source.Count();
            var items = await source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return new PagedAndSortedList<T>(items, count, pageNumber, pageSize, sorted, sortedBy);
        }
    }
}