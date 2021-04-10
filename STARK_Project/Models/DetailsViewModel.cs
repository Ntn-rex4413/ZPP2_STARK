using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STARK_Project.CryptoApiModel;

namespace STARK_Project.Models
{
    public class DetailsViewModel
    {
        public CryptoInfo CryptoModel { get; set; }
        public Dictionary<string, string> Cryptocurrencies { get; set; }
        public Dictionary<string, string> Currencies { get; set; }
    }
}
