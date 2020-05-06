using System.Threading.Tasks;

namespace PostalCodeApi.Domain.Services
{
    public interface IGeoNamesService
    {
        Task ImportAsync(string countryIso);
    }
}