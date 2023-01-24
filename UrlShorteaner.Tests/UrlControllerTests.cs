using AutoFixture;
using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using UrlShortener.Controllers;
using UrlShortener.Data.Entities;
using UrlShortener.Services.Implementation;
using UrlShortener.Services.Interfaces;
using UrlShortener.ViewModels;

namespace UrlShortener.Tests
{
    public class UrlControllerTests
    {
        private Mock<AppUser> mockUser;
        private Mock<IUrlShortenerService> mockService;
        private Mock<ILogger<UrlController>> mockLogger;
        private Mock<IMapper> mockMapper;
        private Mock<UserManager<AppUser>> mockUserManager;
        private Fixture _fixture;
        private UrlController _controller;
        


        public UrlControllerTests()
        {
            mockUser = new Mock<AppUser>();
            mockService = new Mock<IUrlShortenerService>();
            mockLogger = new Mock<ILogger<UrlController>>();
            mockMapper = new Mock<IMapper>();
            mockUserManager = new Mock<UserManager<AppUser>>();
            _fixture = new Fixture();
            
        }

        [Fact]
        public void Get_AllUrl_ReturnStatusOk()
        {
            //Arrange
            var user = _fixture.CreateMany<AppUser>(1).ToList();
            //mockUserManager.Setup(u => u.FindByIdAsync(It.IsAny<AppUser>())).ReturnsAsync(user);
            var urls = _fixture.CreateMany<UrlDetail>(10).ToList();
            mockService.Setup(s => s.GetUrls()).Returns(urls);
            _controller = new UrlController(mockLogger.Object, mockService.Object, null , mockMapper.Object);

            //Act
            var status = _controller.GetAllUrls();
            var result = (OkObjectResult)status.Result;
            var value = (List<UrlDetail>)result.Value;
            

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(value);
            Assert.Equal(urls.Count, value.Count);
            Assert.Equal(urls, value);
            Assert.Equal(urls[7], value[7]);
            
        }

        [Fact]
        public void Get_AllUrl_ReturnBadRequest()
        {
            //Arrange
            mockService.Setup(r => r.GetUrls()).Throws(new Exception()); 
            _controller = new UrlController(mockLogger.Object, mockService.Object, null, mockMapper.Object);

            //Act
            var result = _controller.GetAllUrls();

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
           

        }

        [Fact]
        public void Get_AllUrlAsync_ReturnStatusOk()
        {
            //Arrange
            var user = _fixture.CreateMany<AppUser>(1).ToList();
            //mockUserManager.Setup(u => u.FindByIdAsync(It.IsAny<AppUser>())).ReturnsAsync(user);
            var urls = _fixture.CreateMany<UrlDetail>(10).ToList();
            mockService.Setup(s => s.GetUrlsAsync()).ReturnsAsync(urls);
            _controller = new UrlController(mockLogger.Object, mockService.Object, null, mockMapper.Object);

            //Act
            var status = _controller.GetUrlAsync();
            var result = (OkObjectResult)status.Result;
            var value = (List<UrlDetail>)result.Value;           

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result.Value);
            Assert.Equal(value.Count, urls.Count);

        }

        [Fact]
        public void RedirectToOriginal_ReturnStatus200()
        {
            //Arrange
            var url = _fixture.Create<UrlDetail>();
            mockService.Setup(s => s.RedirectToOriginalUrl(url.Code)).Returns(url.LongUrl);
            _controller = new UrlController(mockLogger.Object, mockService.Object, null, mockMapper.Object);

            //Act
            var result = _controller.RedirectToOriginal(url.Code) as JsonResult;
            var value = result.Value.ToString();
            var str = "{ longUrl = " + url.LongUrl +" }";
            

            //Assert
            Assert.IsType<JsonResult>(result);
            Assert.Equal(str, value);
        }

        //[Fact]
        //public async Task PostUrl_ReturnStatus200()
        //{
        //    //Arrange
        //    var url = _fixture.Create<UrlDetailViewModel>();
        //    mockService.Setup(s => s.GenerateShortUrlAsync(url.LongUrl)).Returns(url.Code);
        //    mockService.Setup(s => s.AddEntity(url));
        //    mockService.Setup(s => s.SaveAll()).Returns(true);
        //    mockUserManager.Setup(u => u.FindByNameAsync(_controller.User.Identity.Name)).Returns(url.User);
        //    _controller = new UrlController(mockLogger.Object, mockService.Object, mockUserManager.Object, mockMapper.Object);

        //    //Act
        //    var result = await _controller.PostUrlTest(url);
            

        //    //Assert
        //    Assert.IsType<OkObjectResult>(result);
        //}

        //[Fact]
        //public async Task Get_UrlById_ReturnOk()
        //{
        //    //Arrange
        //    var urls = _fixture.CreateMany<UrlDetail>(10).ToList();
        //    var id = 5;
        //    mockService.Setup(s => s.GetUrlById(id)).Returns(urls[id]);
        //    _controller = new UrlController(mockLogger.Object, mockService.Object, null, mockMapper.Object);

        //    //Act
        //    var status = _controller.GetUrlById(id);

        //    //Assert
        //    Assert.IsType<OkObjectResult>(status);


        //}


    }
}