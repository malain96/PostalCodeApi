using System.Collections.Generic;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Extensions;

namespace PostalCodeApi.Domain.Services
{
    public interface IPostalCodeService
    {
        Task<PagedAndSortedList<PostalCode>> SearchAsync(int pageNumber, int pageSize, string sort, string code,
            string countryIso);
    }
}