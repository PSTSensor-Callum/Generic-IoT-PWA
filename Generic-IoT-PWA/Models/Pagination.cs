namespace Generic_IoT_PWA.Models
{
    public class Pagination<T>
    {
        public List<T> Data { get; set; }

        public int TotalDataCount { get; set; }
        public int DataCount { get; set; }

        public int Page { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }

        public string? PreviousPage { get; set; }
        public string? NextPage { get; set; }


        public Pagination() { }

        public Pagination(List<T> data, int totalDataCount, int page, int pageCount, int pageSize, string? previousPage, string? nextPage)
        {
            Data = data;

            TotalDataCount = totalDataCount;
            DataCount = data.Count;

            Page = page;
            PageCount = pageCount;
            PageSize = pageSize;

            PreviousPage = previousPage;
            NextPage = nextPage;
        }
    }
}
