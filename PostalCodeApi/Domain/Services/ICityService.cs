using System.Collections.Generic;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Services.Communication;

namespace PostalCodeApi.Domain.Services
{
    public interface ICityService
    {
        Task<CityResponse> FindMatchOrSaveAsync(City city);

        Task<List<City>> GetByPostalCodeAsync(long postalCodeId);
    }
}