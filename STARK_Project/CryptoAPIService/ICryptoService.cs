using STARK_Project.CryptoApiModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace STARK_Project.CryptoAPIService
{
    /// <summary>
    /// Interface for crypto API service
    /// </summary>
    public interface ICryptoService
    {
        Task<CryptoInfo> GetCryptocurrencyInfoAsync(string cryptoSymbol, string currencySymbol);
        Task<CryptoModel> GetCryptocurrenciesInfoAsync();
        Task<CryptoModel> GetCryptocurrenciesInfoAsync(string symbol);
        Task<CryptoHistoricalData> GetHistoricalData(HistoricalDataTypes type,string symbol, string currencySymbol, int? limit, int? aggregate);
        Task<Dictionary<string, string>> GetRankingDataAsync(int limit, string currency);

        Task<Dictionary<string, string>> GetCryptocurrenciesAsync();
        Dictionary<string, string> GetCurrencies();
    }
}
