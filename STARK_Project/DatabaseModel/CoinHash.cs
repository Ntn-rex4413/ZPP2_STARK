using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.DatabaseModel
{
    public class CoinHash : EqualityComparer<Cryptocurreny>
    {
        public override bool Equals(Cryptocurreny x, Cryptocurreny y)
        {
            return x.Symbol.Equals(y.Symbol);
        }

        public override int GetHashCode([DisallowNull] Cryptocurreny obj)
        {
            return base.GetHashCode();
        }
    }
}
