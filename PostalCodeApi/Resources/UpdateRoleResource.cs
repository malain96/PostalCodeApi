using System.ComponentModel.DataAnnotations;

namespace PostalCodeApi.Resources
{
    /// <summary>
    /// Input for updating a user's role
    /// </summary>
    public class UpdateRoleResource
    {
        [Required]
        [RegularExpression("Admin|User",
            ErrorMessage = "The role can either be Admin or User")]
        public string Role { get; set; }
    }
}