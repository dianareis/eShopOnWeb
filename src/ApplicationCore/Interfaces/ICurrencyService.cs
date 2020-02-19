using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public enum Currency {
        USD,
        EUR
    }

    /// <summary>
    /// Abstraction for converting monetary values
    /// </summary>
    public interface ICurrencyService
    {   

        /// <summary>
        /// Convert monetary values from source to target currency
        /// </summary>
        /// <param name="valor">Monetary Value</param>
        /// <param name="source">Source Currency</param>
        /// <param name="target">Target Currency</param>
        /// <param name="cancellationToken">Token used to cancel the operation</param>
        /// <returns></returns>
        Task<decimal> Convert(decimal valor, Currency source, Currency target, CancellationToken cancellationToken = default(CancellationToken));
    }
}