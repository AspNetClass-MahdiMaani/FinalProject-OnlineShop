using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ApplicationServices.Dtos.PersonDtos
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string RedirectUrl { get; set; }
    }
}
