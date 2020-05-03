using System.Collections.Generic;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Services
{
    public interface IPostalCodeService
    {
        Task<IEnumerable<PostalCode>> SearchAsync(int pageNumber, int pageSize, string sort, string code,
            string countryIso);
    }
}