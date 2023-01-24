using System.ComponentModel.DataAnnotations;

namespace UrlShortener.ViewModels
{
    public class RegisterViewModel
    {
        
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Invalid password")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

    }
}
