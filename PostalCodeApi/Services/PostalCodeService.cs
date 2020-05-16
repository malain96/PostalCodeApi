using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        
        public async Task<PostalCodeResponse> FindMatchOrSaveAsync(PostalCode postalCode)
        {
            // Try to retrieve the existing postal code and to return it
            var existingPostalCode = await _postalCodeRepository.FindMatchAsync(postalCode);
            if (existingPostalCode != null)
                return new PostalCodeResponse(existingPostalCode);

            try
            {
                // Try to add the postal code
                await _postalCodeRepository.AddAsync(postalCode);
                await _unitOfWork.CompleteAsync();

                return new PostalCodeResponse(postalCode);
            }
            catch (Exception ex)
            {
                return new PostalCodeResponse($"An error occurred when saving the postal code: {ex.Message}");
            }
        }

        public async Task<PostalCodeResponse> FindMatchOrFail(PostalCode postalCode)
        {
            var existingPostalCode = await _postalCodeRepository.FindMatchAsync(postalCode);

            return existingPostalCode != null
                ? new PostalCodeResponse(existingPostalCode)
                : new PostalCodeResponse("An error occurred when matching the postal code: postal code not found",
                    StatusCodes.Status404NotFound);
        }
    }
}