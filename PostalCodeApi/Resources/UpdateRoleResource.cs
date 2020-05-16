using System.ComponentModel.DataAnnotations;

namespace PostalCodeApi.Resources
{
    public class UpdateRoleResource
    {
        [Required]
        [RegularExpression("Admin|User",
            ErrorMessage = "The role can either be Admin or User")]
        public string Role { get; set; }
    }
}