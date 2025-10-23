namespace Scharff.Domain.Entities
{
    public class ParameterModel
    {
        public int Id { get; set; }
        public int Group_id { get; set; }
        public string? Detail_id { get; set; }
        public string? Description { get; set; }
        public string? Short_description { get; set; }
        public string? Integration_code { get; set; }
        public DateTime? Creation_date { get; set; }
        public string? Creation_author { get; set; }
        public DateTime? Modification_date { get; set; }
        public string? Modification_author { get; set; }
    }
}
