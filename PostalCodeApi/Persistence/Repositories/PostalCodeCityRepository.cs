using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Repositories;
using PostalCodeApi.Persistence.Contexts;

namespace PostalCodeApi.Persistence.Repositories
{
    public class PostalCodeCityRepository : BaseRepository, IPostalCodeCityRepository
    {
        public PostalCodeCityRepository(PostalCodeDbContext context) : base(context)
        {
        }

        public async Task AddAsync(PostalCodeCity postalCodeCity)
        {
            await _context.PostalCodeCities.AddAsync(postalCodeCity);
        }

        public async Task<PostalCodeCity> FindMatchAsync(PostalCodeCity postalCodeCity)
        {
            return await _context.PostalCodeCities.FirstOrDefaultAsync(pcc =>
                pcc.PostalCodeId == postalCodeCity.PostalCodeId && pcc.CityId == postalCodeCity.CityId);
        }

        public async Task<List<PostalCodeCity>> ListByPostalCodeAsync(long postalCodeId)
        {
            return await _context.PostalCodeCities.Where(pcc => pcc.PostalCodeId == postalCodeId).ToListAsync();
        }
    }
}