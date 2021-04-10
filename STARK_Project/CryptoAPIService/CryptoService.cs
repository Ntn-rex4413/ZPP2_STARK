using Newtonsoft.Json;
using STARK_Project.CryptoApiModel;
using STARK_Project.CryptoApiModel.CurrencyEnums;
using STARK_Project.CryptoApiModel.CurrencySymbolsEnums;
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

        private HttpClient _client = new HttpClient();

        public Dictionary<string, string> CryptocurrenciesNames = new Dictionary<string, string>();
        public Dictionary<string, string> CurrenciesNames = new Dictionary<string, string>();

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
            CryptocurrenciesNames.Add("BTC", "Bitcoin");
            CryptocurrenciesNames.Add("LTC", "Litecoin");
            CryptocurrenciesNames.Add("NMC", "Namecoin");
            CryptocurrenciesNames.Add("PPC", "Peercoin");
            CryptocurrenciesNames.Add("DOGE", "Dogecoin");
            CryptocurrenciesNames.Add("GRC", "Gridcoin");
            CryptocurrenciesNames.Add("XPM", "Primecoin");
            CryptocurrenciesNames.Add("XRP", "Ripple");
            CryptocurrenciesNames.Add("NXT", "Nxt");
            CryptocurrenciesNames.Add("AUR", "Auracoin");
            CryptocurrenciesNames.Add("DASH", "Dash");
            CryptocurrenciesNames.Add("NEO", "NEO");
            CryptocurrenciesNames.Add("MZC", "MazaCoin");
            CryptocurrenciesNames.Add("XMR", "Monero");
            CryptocurrenciesNames.Add("TIT", "Titcoin");
            CryptocurrenciesNames.Add("XVG", "Verge");
            CryptocurrenciesNames.Add("XLM", "Stellar");
            CryptocurrenciesNames.Add("VTC", "Vertcoin");
            CryptocurrenciesNames.Add("ETH", "Ethereum");
            CryptocurrenciesNames.Add("ETC", "Ethereum Classic");
            CryptocurrenciesNames.Add("Nano", "Nano");
            CryptocurrenciesNames.Add("USDT", "Tether");
            CryptocurrenciesNames.Add("ZEC", "Zcash");
            CryptocurrenciesNames.Add("BCH", "Bitcoin Cash");
            CryptocurrenciesNames.Add("EOS", "EOS.IO");
            CryptocurrenciesNames.Add("ADA", "Cardano");
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
        public Dictionary<string,string> GetCryptocurrencies()
        {
            return CryptocurrenciesNames;
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
            return await GetCryptoData(
                 string.Join(",", CryptocurrenciesNames.Keys),
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

