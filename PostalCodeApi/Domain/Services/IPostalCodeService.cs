using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Services.Communication;
using PostalCodeApi.Extensions;

namespace PostalCodeApi.Domain.Services
{
    public interface IPostalCodeService
    {
        /// <summary>
        /// Get a filtered, paged and sorted list of postal code
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Number of postal code by page</param>
        /// <param name="sort">Sorting direction</param>
        /// <param name="code">Code to filter by</param>
        /// <param name="countryIso">Country to filter by</param>
        /// <returns>PagedAndSortedList of PostalCode</returns>
        Task<PagedAndSortedList<PostalCode>> SearchAsync(int pageNumber, int pageSize, string sort, string code,
            string countryIso);

        /// <summary>
        /// Find or create a new postal code
        /// </summary>
        /// <param name="postalCode">PostalCode's data</param>
        /// <returns>PostalCode response object</returns>
        Task<PostalCodeResponse> FindMatchOrSaveAsync(PostalCode postalCode);
        
        /// <summary>
        /// Get the matching postal code or fail
        /// </summary>
        /// <param name="postalCode">PostalCode's data</param>
        /// <returns>PostalCode response object</returns>
        Task<PostalCodeResponse> FindMatchOrFail(PostalCode postalCode);
    }
}