using System.Collections.Generic;

namespace Library.RestApi.Model
{
    public class PaginatedData<T>
    {
        public int Total { get; }
        public IEnumerable<T> Data { get; }
        public int Page { get; }

        private PaginatedData(int total, int page, IEnumerable<T> data)
        {
            Total = total;
            Page = page;
            Data = data;
        }

        public static PaginatedData<T> Create(int total, int page, IEnumerable<T> data)
        {
            return new PaginatedData<T>(total, page, data);
        }
    }
}