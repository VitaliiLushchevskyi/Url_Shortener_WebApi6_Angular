using UrlShortener.Data.Entities;

namespace UrlShortener.ViewModels
{
    public class UrlDetailViewModel
    {
        public int UrlId { get; set; }
        public string? LongUrl { get; set; }
        public string? Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public AppUser User { get; set; }
    }
}
