using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.CryptoApiModel
{
    public class CryptoHistoricalData
    {
        public CryptoHistoricalnfo[] Data { get; set; }
        public float TimeFrom { get; set; }
        public float TimeTo { get; set; }
    }
}
