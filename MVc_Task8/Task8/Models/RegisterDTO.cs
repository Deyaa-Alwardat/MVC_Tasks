using System.ComponentModel.DataAnnotations;

namespace Task8.Models
{
    public class RegisterDTO
    {

            [Required]
            public string UserName { get; set; }

            [Required]
            public string Email { get; set; }

            [Required]
            public string PhoneNumber { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }
        }
}
