using PostalCodeApi.Persistence.Contexts;

namespace PostalCodeApi.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly PostalCodeDbContext _context;

        public BaseRepository(PostalCodeDbContext context)
        {
            _context = context;
        }
    }
}