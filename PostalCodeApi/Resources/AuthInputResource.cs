using System.ComponentModel.DataAnnotations;

namespace PostalCodeApi.Resources
{
    /// <summary>
    /// Input to authenticate a user
    /// </summary>
    public class AuthInputResource
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}