using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Extensions;

namespace PostalCodeApi.Domain.Repositories
{
    /// <summary>
    /// Methods to query the PostalCode table
    /// </summary>
    public interface IPostalCodeRepository
    {
        /// <summary>
        /// Sorted page of postal codes 
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Number of postal codes per page</param>
        /// <param name="sort">Sorting direction</param>
        /// /// <param name="code">Code to filter by</param>
        /// <param name="countryIso">Country iso to filter by</param>
        /// <returns>PagedAndSortedList of PostalCode</returns>
        Task<PagedAndSortedList<PostalCode>> SearchAsync(int pageNumber, int pageSize, string sort, string code,
            string countryIso);

        /// <summary>
        /// Create a postal code
        /// </summary>
        /// <param name="postalCode">Postal code to create</param>
        /// <returns>Created postal code</returns>
        Task AddAsync(PostalCode postalCode);

        /// <summary>
        /// Find a match to the given data
        /// </summary>
        /// <param name="postalCode">Postal code's data</param>
        /// <returns>PostalCode or null</returns>
        Task<PostalCode> FindMatchAsync(PostalCode postalCode);
    }
}