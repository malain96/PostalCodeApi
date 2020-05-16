using System.ComponentModel.DataAnnotations;

namespace PostalCodeApi.Resources
{
    /// <summary>
    /// Input for searching a postal code
    /// </summary>
    public class SearchPostalCodeResource
    {
        [Required] public string Code { get; set; }

        [MinLength(2)] [MaxLength(2)] public string CountryIso { get; set; }
    }
}