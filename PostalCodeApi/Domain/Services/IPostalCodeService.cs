using System.Collections.Generic;
using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Services
{
    public interface IPostalCodeService
    {
        Task<IEnumerable<PostalCode>> SearchAsync();
    }
}