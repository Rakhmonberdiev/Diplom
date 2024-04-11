using System.ComponentModel.DataAnnotations;

namespace Diplom.DTO.AuthDtos
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(50, MinimumLength =8)]
        public string Password { get; set; }
    }
}
