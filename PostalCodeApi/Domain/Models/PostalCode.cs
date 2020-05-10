using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostalCodeApi.Domain.Models
{
    public class PostalCode
    {
        public long Id { get; set; }

        [Required] public string Code { get; set; }

        [Required]
        [MaxLength(2)]
        [MinLength(2)]
        public string CountryIso { get; set; }

        public virtual ICollection<PostalCodeCity> PostalCodeCities { get; set; }
    }
}