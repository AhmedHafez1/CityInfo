namespace CityInfo.API.Models
{
    public class PaginationMetadata
    {
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }


        public PaginationMetadata(int pageSize, int totalCount, int currentPage)
        {
            PageSize = pageSize;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        }
    }
}
