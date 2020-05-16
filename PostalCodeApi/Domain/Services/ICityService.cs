using System.Collections.Generic;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Services.Communication;

namespace PostalCodeApi.Domain.Services
{
    public interface ICityService
    {
        /// <summary>
        /// Find or create a new city
        /// </summary>
        /// <param name="city">City's data</param>
        /// <returns>City response object</returns>
        Task<CityResponse> FindMatchOrSaveAsync(City city);

                
        /// <summary>
        /// Get all cities of a postal code
        /// </summary>
        /// <param name="postalCodeId">Postal code's id</param>
        /// <returns>List of cities</returns>
        Task<List<City>> GetByPostalCodeAsync(long postalCodeId);
    }
}