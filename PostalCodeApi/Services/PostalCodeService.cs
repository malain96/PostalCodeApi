using System.Collections.Generic;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Repositories;
using PostalCodeApi.Domain.Services;
using PostalCodeApi.Extensions;

namespace PostalCodeApi.Services
{
    public class PostalCodeService : IPostalCodeService
    {
        private readonly IPostalCodeRepository _postalCodeRepository;

        public PostalCodeService(IPostalCodeRepository postalCodeRepository)
        {
            this._postalCodeRepository = postalCodeRepository;
        }

        public async Task<PagedAndSortedList<PostalCode>> SearchAsync(int pageNumber, int pageSize, string sort, string code,
            string countryIso)
        {
            return await _postalCodeRepository.SearchAsync(pageNumber, pageSize, sort, code, countryIso);
        }
    }
}