﻿using System;
using System.Collections.Generic;
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
            // Try to retrieve the existing postal code city and to return it
            var existingPostalCodeCity = await _postalCodeCityRepository.FindMatchAsync(postalCodeCity);
            if (existingPostalCodeCity != null)
                return new PostalCodeCityResponse(existingPostalCodeCity);

            try
            {
                // Try to add the new link
                await _postalCodeCityRepository.AddAsync(postalCodeCity);
                await _unitOfWork.CompleteAsync();

                return new PostalCodeCityResponse(postalCodeCity);
            }
            catch (Exception ex)
            {
                return new PostalCodeCityResponse($"An error occurred when saving the postal code city: {ex.Message}");
            }
        }
   
        public async Task<List<PostalCodeCity>> GetListByPostalCodeAsync(long postalCodeId)
        {
            return await _postalCodeCityRepository.ListByPostalCodeAsync(postalCodeId);
        }
    }
}