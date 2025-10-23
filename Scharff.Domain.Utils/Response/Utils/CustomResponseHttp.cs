namespace Scharff.Domain.Response.Utils
{
    public class CustomResponseHttp<T>
    {
        public string? message { get; set; }
        public T? data { get; set; }
        public List<string>? error { get; set; }

        public CustomResponseHttp()
        {
            error = new List<string>();
        }

        public CustomResponseHttp(string message, T data)
        {
            this.message = message;
            this.data = data;
        }
    }
}
