using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Repositories;
using PostalCodeApi.Domain.Services;
using PostalCodeApi.Domain.Services.Communication;

namespace PostalCodeApi.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IPostalCodeCityService _postalCodeCityService;
        private readonly IUnitOfWork _unitOfWork;

        public CityService(ICityRepository cityRepository, IPostalCodeCityService postalCodeCityService, IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _postalCodeCityService = postalCodeCityService;
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

        public async Task<List<City>> GetByPostalCodeAsync(long postalCodeId)
        {
            var postalCodeCities = await _postalCodeCityService.GetListByPostalCodeAsync(postalCodeId);
            return await _cityRepository.ListByPostalCodeCityAsync(postalCodeCities);
        }
        
    }
}