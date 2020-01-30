using System.Threading;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Infrastructure.Services.CurrencyServices
{
    public class CurrencyServiceStatic : ICurrencyService
    {
        /// <inheritdoc />
        public Task<decimal> Convert(decimal valor, Currency source, Currency target, CancellationToken cancellationToken = default)
        {
            var convertedValue = valor * 1.25m; // TODO: Apply conversion
            return Task.FromResult(convertedValue);
        }
    }
}