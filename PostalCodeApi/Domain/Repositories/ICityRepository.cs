using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Repositories
{
    public interface ICityRepository
    {
        Task AddAsync(City city);

        Task<City> FindMatchAsync(City city);
    }
}