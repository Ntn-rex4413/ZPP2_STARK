using STARK_Project.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.Models
{
    public class SubscribedCryptoViewModel : Cryptocurreny
    {
        public float UnitPrice { get; set; }

        public SubscribedCryptoViewModel(Cryptocurreny crypto, float unitPrice)
        {
            UnitPrice = unitPrice;
        }
    }
}
