using System.Collections.Generic;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Extensions;

namespace PostalCodeApi.Domain.Repositories
{
    public interface IPostalCodeRepository
    {
        Task<PagedAndSortedList<PostalCode>> SearchAsync(int pageNumber, int pageSize, string sort, string code,
            string countryIso);
    }
}