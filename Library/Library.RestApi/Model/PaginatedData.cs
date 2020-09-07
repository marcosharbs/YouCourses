using System.Collections.Generic;

namespace Library.RestApi.Model
{
    public class PaginatedData<T>
    {
        public int Total { get; }
        public int Page { get; }
        public int PageSize { get; }
        public IEnumerable<T> Data { get; }

        private PaginatedData(int total, int page, int pageSize, IEnumerable<T> data)
        {
            Total = total;
            Page = page;
            Data = data;
            PageSize = pageSize;
        }

        public static PaginatedData<T> Create(int total, int page, int pageSize, IEnumerable<T> data)
        {
            return new PaginatedData<T>(total, page, pageSize, data);
        }
    }
}