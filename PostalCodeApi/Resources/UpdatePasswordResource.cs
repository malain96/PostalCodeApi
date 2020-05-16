using System.ComponentModel.DataAnnotations;

namespace PostalCodeApi.Resources
{
    public class UpdatePasswordResource
    {
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}