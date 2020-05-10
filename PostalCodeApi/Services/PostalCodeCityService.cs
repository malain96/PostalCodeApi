using System;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Repositories;
using PostalCodeApi.Domain.Services;
using PostalCodeApi.Domain.Services.Communication;

namespace PostalCodeApi.Services
{
    public class PostalCodeCityService : IPostalCodeCityService
    {
        private readonly IPostalCodeCityRepository _postalCodeCityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostalCodeCityService(IPostalCodeCityRepository postalCodeCityRepository, IUnitOfWork unitOfWork)
        {
            _postalCodeCityRepository = postalCodeCityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PostalCodeCityResponse> FindMatchOrSaveAsync(PostalCodeCity postalCodeCity)
        {
            var existingPostalCodeCity = await _postalCodeCityRepository.FindMatchAsync(postalCodeCity);

            if (existingPostalCodeCity != null)
                return new PostalCodeCityResponse(existingPostalCodeCity);

            try
            {
                await _postalCodeCityRepository.AddAsync(postalCodeCity);
                await _unitOfWork.CompleteAsync();

                return new PostalCodeCityResponse(postalCodeCity);
            }
            catch (Exception ex)
            {
                return new PostalCodeCityResponse($"An error occurred when saving the postal code city: {ex.Message}");
            }
        }
    }
}