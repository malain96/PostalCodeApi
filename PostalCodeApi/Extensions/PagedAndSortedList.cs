using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PostalCodeApi.Extensions
{
    public class PagedAndSortedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public string Sorted { get; private set; }
        public string SortedBy { get; private set; }

        public List<T> Items { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedAndSortedList(List<T> items, int count, int pageNumber, int pageSize, string sorted, string sortedBy)
        {
            TotalCount = count;
            PageSize = pageSize;
            Sorted = sorted;
            SortedBy = sortedBy;
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            Items = items;
        }

        public static async Task<PagedAndSortedList<T>> ToPagedListAsync(IQueryable<T> source, int pageNumber, int pageSize, string sorted , string sortedBy)
        {
            var count = source.Count();
            var items = await source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return new PagedAndSortedList<T>(items, count, pageNumber, pageSize,sorted, sortedBy);
        }
    }
}