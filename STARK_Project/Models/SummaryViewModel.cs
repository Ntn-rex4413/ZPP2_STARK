using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STARK_Project.CryptoApiModel;

namespace STARK_Project.Models
{
    public class SummaryViewModel
    {
        public CryptoModel CryptoModel { get; set; }
        public Dictionary<string, string> Cryptocurrencies { get; set; }
        public Dictionary<string, string> Currencies { get; set; }
    }
}
