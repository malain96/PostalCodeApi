using System.Threading.Tasks;
using PostalCodeApi.Domain.Repositories;
using PostalCodeApi.Persistence.Contexts;

namespace PostalCodeApi.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostalCodeDbContext _context;

        public UnitOfWork(PostalCodeDbContext context)
        {
            _context = context;     
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}