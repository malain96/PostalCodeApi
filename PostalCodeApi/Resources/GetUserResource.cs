using System.ComponentModel.DataAnnotations;

namespace PostalCodeApi.Resources
{
    public class GetUserResource
    {
        [Required]
        public int Id { get; set; }
    }
}