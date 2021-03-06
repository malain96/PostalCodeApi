﻿namespace PostalCodeApi.Domain.Models
{
    /// <summary>
    /// Database PostalCodeCity model 
    /// </summary>
    public class PostalCodeCity
    {
        public long PostalCodeId { get; set; }

        public long CityId { get; set; }

        public PostalCode PostalCode { get; set; }

        public City City { get; set; }
    }
}