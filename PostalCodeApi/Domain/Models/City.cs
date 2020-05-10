using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostalCodeApi.Domain.Models
{
    public class City
    {
        public long Id { get; set; }

        [Required] public string Name { get; set; }

        public virtual ICollection<PostalCodeCity> PostalCodeCities { get; set; }
    }
}