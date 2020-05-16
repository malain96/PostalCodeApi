using System.ComponentModel.DataAnnotations;

namespace PostalCodeApi.Resources
{
    public class GetByPostalCodeResource
    {
        [Required] public string Code { get; set; }

        [Required] [MinLength(2)] [MaxLength(2)] public string CountryIso { get; set; }
    }
}