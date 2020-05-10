namespace PostalCodeApi.Resources
{
    public class PagedAndSortedRequestResource
    {
        private const int MaxPageSize = 50;

        private int _pageSize = 10;

        private string _sort = "asc";
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public string Sort
        {
            get => _sort;
            set => _sort = value.ToLowerInvariant() == "desc" ? value : _sort;
        }
    }
}