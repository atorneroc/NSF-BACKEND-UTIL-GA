namespace Scharff.API.Utils.Utils.Models
{
    public class CustomResponse<T>
    {
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Error { get; set; }

        public CustomResponse()
        {
            Error = new List<string>();
        }

        public CustomResponse(string message, T data)
        {
            Message = message;
            Data = data;
        }
    }
}
