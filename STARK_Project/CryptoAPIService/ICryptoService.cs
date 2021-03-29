using STARK_Project.CryptoApiModel;
using STARK_Project.CryptoApiModel.CurrencyEnums;
using STARK_Project.CryptoApiModel.CurrencySymbolsEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.CryptoAPIService
{
    interface ICryptoService
    {
       CryptoInfo GetCryptocurrencyInfo(CryptocurrencySymbols cryptoSymbol, CurrencySymbols currencySymbol);
        CryptoModel GetCryptocurrenciesInfo();
    }
}
