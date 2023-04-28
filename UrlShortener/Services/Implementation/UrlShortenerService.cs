using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Data.Entities;
using UrlShortener.Services.Interfaces;
using UrlShortener.ViewModels;

namespace UrlShortener.Services.Implementation
{
    public class UrlShortenerService : IUrlShortenerService
    {
        const string safeShortCode = "qwertyuiopasdfghjklzxcvbnm1234567890";
        readonly AppDbContext _context;
        public UrlShortenerService(AppDbContext context)
        {
            _context = context;
        }

        public string GenerateShortUrlAsync(string longUrl)
        {
            string code =  GenerateCode();
            while (_context.UrlDetails.ToList().Where(x => x.Code == code).Any())
            {
                code = GenerateCode();
            }   

            return code;

        }

        public string GenerateShortUrl(string longUrl)
        {
            var code = GenerateCode();
            while (_context.UrlDetails.ToList().Where(x => x.Code == code).Any())
            {
                code = GenerateCode();
            }

            var url = new UrlDetail()
            {
                CreatedDate = DateTime.Now,
                LongUrl = longUrl,
                Code = code,

            };

            _context.UrlDetails.Add(url);
            _context.SaveChanges();

            return code;

        }

        public string GenerateCode()
        {
            return safeShortCode.Substring(new Random().Next(0, safeShortCode.Length), new Random().Next(3, 5));
        }

        public string RedirectToOriginalUrl(string code)
        {

            return _context.UrlDetails.Where(x => x.Code == code).FirstOrDefault().LongUrl;
        }

        public IEnumerable<UrlDetail> GetUrls()
        {
            return _context.UrlDetails.OrderBy(p => p.Id).ToList();
        }

        

        public UrlDetail GetUrlById(int id)
        {
            return _context.UrlDetails.Include(p=>p.User).Where(u => u.Id == id).FirstOrDefault()!;
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<List<UrlDetail>> GetUrlsAsync()
        {
            return await _context.UrlDetails.OrderBy(p => p.Id).ToListAsync();
        }
    }
}
