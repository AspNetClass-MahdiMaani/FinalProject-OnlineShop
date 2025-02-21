using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ApplicationServices.Dtos.PersonDtos
{
    public class CreateUserModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RePassword { get; set; }
    }
}
