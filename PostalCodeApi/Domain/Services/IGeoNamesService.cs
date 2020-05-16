using System.Threading.Tasks;

namespace PostalCodeApi.Domain.Services
{
    public interface IGeoNamesService
    {
        /// <summary>
        /// Import all postal codes and cities of a country
        /// </summary>
        /// <param name="countryIso">Country iso</param>
        /// <returns>Task</returns>
        Task ImportAsync(string countryIso);
    }
}