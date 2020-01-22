using System.Threading;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;


    public class CurrencyServiceExternal : ICurrencyService
    {
        public Task<decimal> Convert(decimal valor, Currency source, Currency target, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
