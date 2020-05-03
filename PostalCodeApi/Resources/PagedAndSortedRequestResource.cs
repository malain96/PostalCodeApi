namespace PostalCodeApi.Resources
{
    public class PagedAndSortedRequestResource
    {
        const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        private string _sort = "asc";

        public string Sort
        {
            get => _sort;
            set => _sort = value.ToLowerInvariant() == "desc" ? value : _sort;
        }
    }
}