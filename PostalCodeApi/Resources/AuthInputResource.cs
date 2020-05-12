using System.ComponentModel.DataAnnotations;

namespace PostalCodeApi.Resources
{
    public class AuthInputResource
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}