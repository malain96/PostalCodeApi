using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Repositories
{
    public interface IPostalCodeCityRepository
    {
        Task AddAsync(PostalCodeCity postalCodeCity);

        Task<PostalCodeCity> FindMatchAsync(PostalCodeCity postalCodeCity);
    }
}