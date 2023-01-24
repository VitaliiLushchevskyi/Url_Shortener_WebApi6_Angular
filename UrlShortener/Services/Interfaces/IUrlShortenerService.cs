using UrlShortener.Data.Entities;

namespace UrlShortener.Services.Interfaces
{
    public interface IUrlShortenerService
    {
        string RedirectToOriginalUrl(string code);
        string GenerateShortUrl(string longUrl);
        string GenerateCode();
        string GenerateShortUrlAsync(string longUrl);
        IEnumerable<UrlDetail> GetUrls();
        Task<List<UrlDetail>> GetUrlsAsync();
        UrlDetail GetUrlById(int id);
        void AddEntity(object model);
        bool SaveAll();
    }
}
