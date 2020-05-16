using System.Collections.Generic;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Services.Communication;

namespace PostalCodeApi.Domain.Services
{
    public interface IPostalCodeCityService
    {
        Task<PostalCodeCityResponse> FindMatchOrSaveAsync(PostalCodeCity postalCodeCity);

        Task<List<PostalCodeCity>> GetListByPostalCodeAsync(long postalCodeId);
    }
}