using System.ComponentModel.DataAnnotations;

namespace PostalCodeApi.Resources
{
    /// <summary>
    /// Code and country iso to get by postal code
    /// </summary>
    public class GetByPostalCodeResource
    {
        [Required] public string Code { get; set; }

        [Required] [MinLength(2)] [MaxLength(2)] public string CountryIso { get; set; }
    }
}