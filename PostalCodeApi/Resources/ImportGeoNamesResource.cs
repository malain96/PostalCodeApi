using System.ComponentModel.DataAnnotations;

namespace PostalCodeApi.Resources
{
    /// <summary>
    /// Country iso for the GeoNames import
    /// </summary>
    public class ImportGeoNamesResource
    {
        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        public string CountryIso { get; set; }
    }
}