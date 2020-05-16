using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Repositories
{
    public interface IPostalCodeCityRepository
    {
        Task AddAsync(PostalCodeCity postalCodeCity);

        Task<PostalCodeCity> FindMatchAsync(PostalCodeCity postalCodeCity);

        Task<List<PostalCodeCity>> ListByPostalCodeAsync(long postalCodeId);
    }
}