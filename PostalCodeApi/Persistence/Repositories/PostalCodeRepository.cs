using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Repositories;
using PostalCodeApi.Persistence.Contexts;

namespace PostalCodeApi.Persistence.Repositories
{
    public class PostalCodeRepository : BaseRepository, IPostalCodeRepository
    {
        public PostalCodeRepository(PostalCodeDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PostalCode>> SearchAsync()
        {
            return await _context.PostalCodes.ToListAsync();
        }
    }
}