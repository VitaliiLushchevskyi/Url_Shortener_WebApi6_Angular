using UrlShortener.Data.Entities;

namespace UrlShortener.Data.Dto
{
    public class UrlDto
    {
        public string longUrl { get; set; }
        public AppUser? User { get; set; }
    }
}
