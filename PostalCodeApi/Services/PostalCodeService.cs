using System.Collections.Generic;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Repositories;
using PostalCodeApi.Domain.Services;

namespace PostalCodeApi.Services
{
    public class PostalCodeService : IPostalCodeService
    {
        private readonly IPostalCodeRepository _postalCodeRepository;

        public PostalCodeService(IPostalCodeRepository postalCodeRepository)
        {
            this._postalCodeRepository = postalCodeRepository;
        }
        
        public async Task<IEnumerable<PostalCode>> SearchAsync()
        {
            return await _postalCodeRepository.SearchAsync();
        }
    }
}