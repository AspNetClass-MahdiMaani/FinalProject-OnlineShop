using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ApplicationServices.Dtos.PersonDtos
{
    public class CreateRoleModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
