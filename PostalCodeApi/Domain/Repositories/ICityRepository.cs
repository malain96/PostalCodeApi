using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Repositories
{
    /// <summary>
    /// Methods to query the City table
    /// </summary>
    public interface ICityRepository
    {
        /// <summary>
        /// Create a city
        /// </summary>
        /// <param name="city">City to create</param>
        /// <returns>Created city</returns>
        Task AddAsync(City city);

        /// <summary>
        /// Find a match to the given data
        /// </summary>
        /// <param name="city">City's data</param>
        /// <returns>City or null</returns>
        Task<City> FindMatchAsync(City city);

        /// <summary>
        /// Get a list of city for the given links
        /// </summary>
        /// <param name="postalCodeCities">Links' data</param>
        /// <returns>List of City</returns>
        Task<List<City>> ListByPostalCodeCityAsync(IEnumerable<PostalCodeCity> postalCodeCities);
    }
}