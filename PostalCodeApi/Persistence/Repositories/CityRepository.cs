using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Repositories;
using PostalCodeApi.Persistence.Contexts;

namespace PostalCodeApi.Persistence.Repositories
{
    public class CityRepository : BaseRepository, ICityRepository

    {
        public CityRepository(PostalCodeDbContext context) : base(context)
        {
        }

        public async Task AddAsync(City city)
        {
            await _context.Cities.AddAsync(city);
        }

        public async Task<City> FindMatchAsync(City city)
        {
            return await _context.Cities.FirstOrDefaultAsync(c => c.Name == city.Name);
        }
    }
}