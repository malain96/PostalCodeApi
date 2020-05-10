﻿using System;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Repositories;
using PostalCodeApi.Domain.Services;
using PostalCodeApi.Domain.Services.Communication;

namespace PostalCodeApi.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CityService(ICityRepository cityRepository, IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CityResponse> FindMatchOrSaveAsync(City city)
        {
            var existingCity = await _cityRepository.FindMatchAsync(city);

            if (existingCity != null)
                return new CityResponse(existingCity);

            try
            {
                await _cityRepository.AddAsync(city);
                await _unitOfWork.CompleteAsync();

                return new CityResponse(city);
            }
            catch (Exception ex)
            {
                return new CityResponse($"An error occurred when saving the city: {ex.Message}");
            }
        }
    }
}