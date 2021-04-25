using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STARK_Project.DatabaseModel;

namespace STARK_Project.Models
{
    public class SubscriptionsViewModel
    {
        public List<SubscribedCryptoViewModel> WatchedCryptocurrencies { get; set; }
        public Dictionary<string, string> Cryptocurrencies { get; set; }
        public Dictionary<string, string> Currencies { get; set; }
    }
}
