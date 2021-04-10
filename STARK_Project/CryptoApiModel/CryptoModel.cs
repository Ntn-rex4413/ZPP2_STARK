﻿using STARK_Project.CryptoApiModel.CurrencyEnums;
using STARK_Project.CryptoApiModel.CurrencySymbolsEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.CryptoApiModel
{
    /// <summary>
    /// Main model from cryptoApi
    /// </summary>
    public class CryptoModel
    {/// <summary>
    ///  Enter cryptocurrency and currency info to get info about this cryptocurrency
    /// </summary>
        public Dictionary<CryptocurrencySymbols, Dictionary<CurrencySymbols, CryptoInfo>> RAW { get; set; }
    }
}