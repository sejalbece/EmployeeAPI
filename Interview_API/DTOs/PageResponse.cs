namespace Interview_API.DTOs
{
    public class PageResponse<T>
    {
        public List<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }


    }
}
