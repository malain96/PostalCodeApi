using System.ComponentModel.DataAnnotations;

namespace PostalCodeApi.Resources
{
    public class ImportGeoNamesResource
    {
        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        public string CountryIso { get; set; }
    }
}