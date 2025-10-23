namespace Scharff.Domain.Response.Utils
{
    public class PaginatedResponse<T>
    {
        public IEnumerable<T>? Result { get; set; }
        public int Total_rows { get; set; }
    }
}
