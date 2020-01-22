using System.Threading;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Infrastructure.Services.CurrencyServices
{
    public class CurrencyServiceStatic : ICurrencyService
    {
        public Task<decimal> Convert(decimal valor, Currency source, Currency target, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(valor);
        }
    }
}