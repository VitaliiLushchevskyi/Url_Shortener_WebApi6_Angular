namespace UrlShortener.Data.Entities
{
    public class UrlDetail
    {
        public int Id { get; set; }
        public string LongUrl { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public AppUser User { get; set; }
    }
}
