using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ApplicationServices.Dtos.PersonDtos
{
    public class InsertPersonDto
    {
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
    }
}
