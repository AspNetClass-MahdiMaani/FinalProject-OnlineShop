using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ApplicationServices.Dtos.PersonDtos
{
    public class EditUserRoles
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public List<string> UserRoles { get; set; }
        [Required]
        public List<IdentityRole> Roles { get; set; }
    }
}
