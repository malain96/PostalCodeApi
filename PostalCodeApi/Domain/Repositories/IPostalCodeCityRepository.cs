using System.Collections.Generic;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Repositories
{
    /// <summary>
    /// Methods to query the PostalCodeCity table
    /// </summary>
    public interface IPostalCodeCityRepository
    {
        /// <summary>
        /// Create a postal code/city link
        /// </summary>
        /// <param name="postalCodeCity">Postal code/city link to create</param>
        /// <returns>Created postal code/city link</returns>
        Task AddAsync(PostalCodeCity postalCodeCity);

        /// <summary>
        /// Find a match to the given data
        /// </summary>
        /// <param name="postalCodeCity">Link's data</param>
        /// <returns>PostalCodeCity or null</returns>
        Task<PostalCodeCity> FindMatchAsync(PostalCodeCity postalCodeCity);

        /// <summary>
        /// Get a list of links with the given postal code
        /// </summary>
        /// <param name="postalCodeId">Postal code's id</param>
        /// <returns>List of PostalCode</returns>
        Task<List<PostalCodeCity>> ListByPostalCodeAsync(long postalCodeId);
    }
}