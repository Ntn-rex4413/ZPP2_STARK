using Newtonsoft.Json;
using STARK_Project.CryptoApiModel;
using STARK_Project.CryptoApiModel.CurrencyEnums;
using STARK_Project.CryptoApiModel.CurrencySymbolsEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace STARK_Project.CryptoAPIService
{

    public class CryptoService : ICryptoService
    {

        private static readonly string _apiKey = "f55d85f81594925184304042a6bac7d8ee60a570722469d1ba0a3cee4ed6f959";
        private readonly string _baseURL = "https://min-api.cryptocompare.com/";
        private static readonly string _mulitInfoSubUri = "data/pricemultifull";

        private HttpClient _client = new HttpClient();

        public CryptoService()
        {
            _client.BaseAddress = new Uri(_baseURL);
            _client.DefaultRequestHeaders.Add("Apikey", _apiKey);
        }
        /// <summary>
        /// Get Info about all cryptocurrency and their conversion into currencies included in CurrencySymbolsEnums
        /// </summary>
        /// <returns>return Model about all cryptocurrency</returns>
        public async Task<CryptoModel> GetCryptocurrenciesInfoAsync()
        {
            return await GetCryptoData(
                        string.Join(",", Enum.GetNames(typeof(CryptocurrencySymbols))),
                        string.Join(",", Enum.GetNames(typeof(CurrencySymbols)))
                        );
        }
        /// <summary>
        /// Get Info about all cryptocurrency and their conversion into currencies with specific symbol, included in CurrencySymbolsEnums
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns>return Model  about all cryptocurrency in specific currency</returns>
        public async Task<CryptoModel> GetCryptocurrenciesInfoAsync(CurrencySymbols symbol)
        {
            return await GetCryptoData(
                 string.Join(",", Enum.GetNames(typeof(CryptocurrencySymbols))),
                 symbol.ToString());
        }
        /// <summary>
        /// Get info about specific cryptocurrency in specific currency
        /// </summary>
        /// <param name="cryptoSymbol"></param>
        /// <param name="currencySymbol"></param>
        /// <returns>returns Model about cryptocurrency in specific currency</returns>
        public async Task<CryptoInfo> GetCryptocurrencyInfoAsync(CryptocurrencySymbols cryptoSymbol, CurrencySymbols currencySymbol)
        {
            var data =  await GetCryptoData(
           cryptoSymbol.ToString(),
           currencySymbol.ToString());
            if(data is null) return null;
            return data.RAW[cryptoSymbol][currencySymbol];
           
        }


        private async Task<CryptoModel> GetCryptoData(string cryptoSymbols, string currencySymbols)
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("fsyms", cryptoSymbols),
                new KeyValuePair<string, string>("tsyms", currencySymbols)
            };
            var response = await GetResponse(_mulitInfoSubUri, parameters);

            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<CryptoModel>(await response.Content.ReadAsStringAsync());
                return data;
            }
            return null;
        }

        private async Task<HttpResponseMessage> GetResponse(string uri, List<KeyValuePair<string, string>> parameters)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (var parameter in parameters)
                query.Add(parameter.Key, parameter.Value);

            uriBuilder.Query = query.ToString();

            return await _client.GetAsync(uriBuilder.Uri);
        }
    }
}

