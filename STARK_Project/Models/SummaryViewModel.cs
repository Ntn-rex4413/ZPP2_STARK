using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STARK_Project.CryptoApiModel;

namespace STARK_Project.Models
{
    public class SummaryViewModel
    {
        public CryptoModel CryptoModel { get; }
        public Dictionary<string, string> Cryptocurrencies { get; }
        public Dictionary<string, string> Currencies { get; }
    }
}
