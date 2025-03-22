using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ApplicationServices.Dtos.PersonDtos
{
    public class PutPersonServiceDto
    {
        public Guid Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
}
