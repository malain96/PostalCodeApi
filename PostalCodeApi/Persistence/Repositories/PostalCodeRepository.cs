using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Repositories;
using PostalCodeApi.Extensions;
using PostalCodeApi.Persistence.Contexts;

namespace PostalCodeApi.Persistence.Repositories
{
    public class PostalCodeRepository : BaseRepository, IPostalCodeRepository
    {
        public PostalCodeRepository(PostalCodeDbContext context) : base(context)
        {
        }

        public async Task<PagedAndSortedList<PostalCode>> SearchAsync(int pageNumber, int pageSize, string sort,
            string code,
            string countryIso)
        {
            IQueryable<PostalCode> postalCodes = _context.PostalCodes.Include(pc => pc.PostalCodeCities)
                .ThenInclude(pcc => pcc.City).Where(pc =>
                    (string.IsNullOrEmpty(code) || pc.Code.StartsWith(code)) &&
                    (string.IsNullOrEmpty(countryIso) || pc.CountryIso == countryIso));
            postalCodes = sort.ToLowerInvariant() == "desc"
                ? postalCodes.OrderByDescending(postalCode => postalCode.Code)
                : postalCodes.OrderBy(postalCode => postalCode.Code);
            return await PagedAndSortedList<PostalCode>.ToPagedListAsync(postalCodes, pageNumber, pageSize,
                sort.ToLowerInvariant(), "code");
        }
    }
}