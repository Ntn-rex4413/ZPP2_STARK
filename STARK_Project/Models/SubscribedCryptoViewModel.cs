using STARK_Project.CryptoApiModel;
using STARK_Project.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.Models
{
    public class SubscribedCryptoViewModel : Cryptocurrency
    {
        public string ToSymbol { get; set; }
        public float UnitPrice { get; set; }

        public SubscribedCryptoViewModel(Cryptocurrency crypto, CryptoInfo info)
        {
            Id = crypto.Id;
            Name = crypto.Name;
            Symbol = crypto.Symbol;
            ToSymbol = info.ToSymbol;
            UnitPrice = info.Price;
        }
    }
}
