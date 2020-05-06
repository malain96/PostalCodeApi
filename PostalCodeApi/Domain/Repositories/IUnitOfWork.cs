using System.Threading.Tasks;

namespace PostalCodeApi.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}