﻿using System.Collections.Generic;

namespace PostalCodeApi.Resources
{
    public class PagedAndSortedResponseResource<T>
    {
        public List<T> Items { get; set; }
        public string Sorted { get; set; }
        public string SortedBy { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
    }
}