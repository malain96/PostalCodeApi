using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Services.Communication;

namespace PostalCodeApi.Domain.Services
{
    public interface IPostalCodeCityService
    {
        Task<SavePostalCodeCityResponse> FindMatchOrSaveAsync(PostalCodeCity postalCodeCity);
    }
}