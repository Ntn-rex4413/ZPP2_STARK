using STARK_Project.CryptoApiModel.CurrencyEnums;
using STARK_Project.CryptoApiModel.CurrencySymbolsEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.CryptoApiModel
{
    public class CryptoModel
    {
        public Dictionary<CryptocurrencySymbols, Dictionary<CurrencySymbols, CryptoInfo>> RAW { get; set; }
    }
}
