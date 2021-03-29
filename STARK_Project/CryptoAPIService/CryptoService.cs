using STARK_Project.CryptoApiModel;
using STARK_Project.CryptoApiModel.CurrencyEnums;
using STARK_Project.CryptoApiModel.CurrencySymbolsEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace STARK_Project.CryptoAPIService
{

    public class CryptoService : ICryptoService
    {
        private static readonly string _apiKey = "f55d85f81594925184304042a6bac7d8ee60a570722469d1ba0a3cee4ed6f959";
        private  HttpClient _client = new HttpClient();

        public CryptoModel GetCryptocurrenciesInfo()
        {
            throw new NotImplementedException();
        }

        public CryptoInfo GetCryptocurrencyInfo(CryptocurrencySymbols cryptoSymbol, CurrencySymbols currencySymbol)
        {
            throw new NotImplementedException();
        }
    }
}
