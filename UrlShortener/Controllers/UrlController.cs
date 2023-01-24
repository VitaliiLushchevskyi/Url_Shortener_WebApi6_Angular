using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using UrlShortener.Data.Dto;
using UrlShortener.Data.Entities;
using UrlShortener.Services.Interfaces;
using UrlShortener.ViewModels;

namespace UrlShortener.Controllers
{
    [Route("api/[controller]")]
    public class UrlController : Controller
    {
        private readonly ILogger<UrlController> _logger;
        private readonly IUrlShortenerService _urlShortenerService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;


        public UrlController(ILogger<UrlController> logger, IUrlShortenerService urlShortener, UserManager<AppUser> userManager, IMapper mapper)
        {
            _urlShortenerService = urlShortener;
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("RedirectToOriginal/{code}")]
        public ActionResult RedirectToOriginal(string code)
        {
            var longUrl = _urlShortenerService.RedirectToOriginalUrl(code);

            return Json(new { longUrl } );

        }

        [HttpGet]
        public ActionResult<IEnumerable<UrlDetail>> GetAllUrls()
        {
            try
            {
                return Ok(_urlShortenerService.GetUrls());                                                                                                                                                                                             
            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get urls: {ex}");
                return BadRequest("Failed to get urls");
            }
        }

        [HttpPost("Test")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostUrlTest([FromBody] UrlDetailViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var code = _urlShortenerService.GenerateShortUrlAsync(model.LongUrl);
                    var newUrl = new UrlDetail()
                    {
                        CreatedDate = DateTime.Now,
                        Code = code,
                        LongUrl = model.LongUrl,
                        User = await _userManager.FindByNameAsync(User.Identity.Name)
                    };
                    _urlShortenerService.AddEntity(newUrl);
                    if (_urlShortenerService.SaveAll())
                    {
                        return Ok(newUrl.Code);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }

            catch (Exception ex)
            {

                _logger.LogError($"Failed to get short url: {ex}");
                return BadRequest("Failed to get short url!");
            }
            return BadRequest("Failed to save a new url");
        }
        

        [HttpPost]
        public async Task<IActionResult> PostUrl([FromBody] UrlDetailViewModel url)
        {
            try
            {
                var newUrl = _mapper.Map<UrlDetailViewModel, UrlDetail>(url);
   
                return Ok(_urlShortenerService.GenerateShortUrl(newUrl.LongUrl));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get short url: {ex}");
                return BadRequest("Failed to get short url!");
            }

        }

        [HttpGet]
        [Route("GetUrlById/{id}")]
        public IActionResult GetUrlById(int id)
        {
            try
            {
                var url = _urlShortenerService.GetUrlById(id);
                if (url != null)
                {
                    return Ok(_mapper.Map<UrlDetail, UrlDetailViewModel>(url));
                }
                else { return NotFound(); }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get url {ex}");
                return BadRequest("Failed to get url");
            }
        }

        [HttpGet]
        [Route("GetAllUrl")]
        public async Task<IActionResult> GetUrlAsync()
        {
            try
            {
                return Ok(await _urlShortenerService.GetUrlsAsync());
            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get urls: {ex}");
                return BadRequest("Failed to get urls");
            }
        }
      
    }
}
