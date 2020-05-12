using System.ComponentModel.DataAnnotations;

namespace PostalCodeApi.Resources
{
    public class UpdateIsAdminResource
    {
        [Required]
        public bool IsAdmin { get; set; }
    }
}