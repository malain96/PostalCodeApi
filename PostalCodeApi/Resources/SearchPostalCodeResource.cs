using System.ComponentModel.DataAnnotations;

namespace PostalCodeApi.Resources
{
    public class SearchPostalCodeResource
    {
        [Required] 
        public string Code { get; set; }

        [MinLength(2)] 
        [MaxLength(2)] 
        public string CountryIso { get; set; }
    }
}