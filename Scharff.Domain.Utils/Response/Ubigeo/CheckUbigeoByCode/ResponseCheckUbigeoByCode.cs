namespace Scharff.Domain.Response.Ubigeo.CheckUbigeoByCode
{
    public class ResponseCheckUbigeoByCode
    {
        public int Id { get; set; }
        public int Parent_id { get; set; }
        public string? Description { get; set; }
        public string? UbigeoCode { get; set; }
        public string? Level { get; set; }
        public string? Label { get; set; }
    }
}