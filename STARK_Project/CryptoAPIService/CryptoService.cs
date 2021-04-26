using Newtonsoft.Json;
using STARK_Project.CryptoApiModel;

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static readonly string _baseCryptoInfoSubUri = "data/all/coinlist";

        private HttpClient _client = new HttpClient();

        private  Dictionary<string, string> CryptocurrenciesNames = new Dictionary<string, string>();
        private  Dictionary<string, string> CurrenciesNames = new Dictionary<string, string>();

        public CryptoService()
        {
            _client.BaseAddress = new Uri(_baseURL);
            _client.DefaultRequestHeaders.Add("Apikey", _apiKey);
            InitializeCryptocurrencies();
            IntializeCurrencies();
        }

        /// <summary>
        /// add new values to dictionary
        /// </summary>
        private void InitializeCryptocurrencies()
        {
            CryptocurrenciesNames = GetCryptocurrenciesAsync().Result;
        }

        /// <summary>
        /// add new values to dictionary
        /// </summary>
        private void IntializeCurrencies()
        {
            CurrenciesNames.Add("PLN", "Polski złoty");
            CurrenciesNames.Add("EUR", "Euro");
            CurrenciesNames.Add("USD", "United States dollar");
        }

        /// <summary>
        /// returns cryptocurrencies dictionary
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> GetCryptocurrenciesAsync()
        {
            var result = new Dictionary<string, string>();

            var request = await GetResponse(_baseURL + _baseCryptoInfoSubUri, new List<KeyValuePair<string, string>>());
            if (request.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<CryptoModel>(await request.Content.ReadAsStringAsync());
                foreach (var coin in data.Data.Values)
                    result.Add(coin.Symbol, coin.CoinName);
            }
            return result;
        }

        /// <summary>
        /// return currencies dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetCurrencies()
        {
            return CurrenciesNames;
        }

        /// <summary>
        /// Get Info about all cryptocurrency and their conversion into currencies included in CurrencySymbolsEnums
        /// </summary>
        /// <returns>return Model about all cryptocurrency</returns>
        public async Task<CryptoModel> GetCryptocurrenciesInfoAsync()
        {
            return await GetCryptoData(
                        string.Join(",", CryptocurrenciesNames.Keys),
                        string.Join(",", CurrenciesNames.Keys)
                        );
        }
        /// <summary>
        /// Get Info about all cryptocurrency and their conversion into currencies with specific symbol, included in CurrencySymbolsEnums
        /// </summary>
        /// <param name="currencySymbol"></param>
        /// <returns>return Model  about all cryptocurrency in specific currency</returns>
        public async Task<CryptoModel> GetCryptocurrenciesInfoAsync(string currencySymbol)
        {
            Debug.WriteLine(CryptocurrenciesNames.Count - (CryptocurrenciesNames.Count - 30));
            return await GetCryptoData(
                 string.Join(",", CryptocurrenciesNames.Keys.SkipLast(CryptocurrenciesNames.Count - 30)),
                 currencySymbol.ToString());
        }
        /// <summary>
        /// Get info about specific cryptocurrency in specific currency
        /// </summary>
        /// <param name="cryptoSymbol"></param>
        /// <param name="currencySymbol"></param>
        /// <returns>returns Model about cryptocurrency in specific currency</returns>
        public async Task<CryptoInfo> GetCryptocurrencyInfoAsync(string cryptoSymbol, string currencySymbol)
        {
            var data = await GetCryptoData(
           cryptoSymbol.ToString(),
           currencySymbol.ToString());
            if (data is null) return null;
            return data.RAW[cryptoSymbol][currencySymbol];

        }


        private async Task<CryptoModel> GetCryptoData(string cryptoSymbols, string currencySymbols)
        {

            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("fsyms", cryptoSymbols),
                new KeyValuePair<string, string>("tsyms", currencySymbols)
            };
            var response = await GetResponse(_baseURL + _mulitInfoSubUri, parameters);

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
            Debug.WriteLine(uriBuilder.Uri);
            return await _client.GetAsync(uriBuilder.Uri);
        }
    }
}

