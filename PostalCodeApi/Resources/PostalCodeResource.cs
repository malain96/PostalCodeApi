using System.Collections.Generic;

namespace PostalCodeApi.Resources
{
    public class PostalCodeResource
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string CountryIso { get; set; }

        public IList<CityResource> Cities { get; set; }
    }
}