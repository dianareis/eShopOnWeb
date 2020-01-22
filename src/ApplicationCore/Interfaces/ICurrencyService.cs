using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public enum Currency {
        USD,
        EUR,
        GBP
    }

    public interface ICurrencyService
    {   
        Task<decimal> Convert(decimal valor, Currency source, Currency target, CancellationToken cancellationToken = default(CancellationToken));
    }
}
