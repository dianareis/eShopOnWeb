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
            decimal rate = 1m;

            if(target == Currency.EUR) {
                rate = 1.05m;
            }

            var convertedValue = valor * rate;
            return Task.FromResult(convertedValue);
        }
    }
}