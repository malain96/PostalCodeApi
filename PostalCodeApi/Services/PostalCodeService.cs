using System;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Repositories;
using PostalCodeApi.Domain.Services;
using PostalCodeApi.Domain.Services.Communication;
using PostalCodeApi.Extensions;

namespace PostalCodeApi.Services
{
    public class PostalCodeService : IPostalCodeService
    {
        private readonly IPostalCodeRepository _postalCodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostalCodeService(IPostalCodeRepository postalCodeRepository, IUnitOfWork unitOfWork)
        {
            _postalCodeRepository = postalCodeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedAndSortedList<PostalCode>> SearchAsync(int pageNumber, int pageSize, string sort,
            string code,
            string countryIso)
        {
            return await _postalCodeRepository.SearchAsync(pageNumber, pageSize, sort, code, countryIso);
        }

        public async Task<SavePostalCodeResponse> FindMatchOrSaveAsync(PostalCode postalCode)
        {
            var existingPostalCode = await _postalCodeRepository.FindMatchAsync(postalCode);

            if (existingPostalCode != null)
                return new SavePostalCodeResponse(existingPostalCode);

            try
            {
                await _postalCodeRepository.AddAsync(postalCode);
                await _unitOfWork.CompleteAsync();

                return new SavePostalCodeResponse(postalCode);
            }
            catch (Exception ex)
            {
                return new SavePostalCodeResponse($"An error occurred when saving the postal code: {ex.Message}");
            }
        }
    }
}