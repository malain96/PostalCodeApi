using System.Collections.Generic;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Services.Communication;

namespace PostalCodeApi.Domain.Services
{
    public interface IPostalCodeCityService
    {
        /// <summary>
        /// Find or create a new postalCodeCity
        /// </summary>
        /// <param name="postalCodeCity">PostalCodeCity's data</param>
        /// <returns>PostalCodeCity response object</returns>
        Task<PostalCodeCityResponse> FindMatchOrSaveAsync(PostalCodeCity postalCodeCity);

        /// <summary>
        /// Get a list of postalCodeCity which have the given postal code
        /// </summary>
        /// <param name="postalCodeId">PostalCode's id</param>
        /// <returns>List of PostalCodeCity</returns>
        Task<List<PostalCodeCity>> GetListByPostalCodeAsync(long postalCodeId);
    }
}