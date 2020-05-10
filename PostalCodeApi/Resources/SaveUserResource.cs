using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostalCodeApi.Resources
{
    public class SaveUserResource
    {
        [Required] 
        public string Username { get; set; }
        
        public bool IsAdmin { get; set; } = false;

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [NotMapped]
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}