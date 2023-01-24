using System.ComponentModel.DataAnnotations;

namespace UrlShortener.ViewModels
{
    public class AppUserViewModel
    {
        public string? Id { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
