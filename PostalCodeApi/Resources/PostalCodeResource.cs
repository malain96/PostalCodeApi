using System.Collections.Generic;

namespace PostalCodeApi.Resources
{
    /// <summary>
    /// PostalCode with a list of CityResource
    /// </summary>
    public class PostalCodeResource
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string CountryIso { get; set; }

        public IList<CityResource> Cities { get; set; }
    }
}