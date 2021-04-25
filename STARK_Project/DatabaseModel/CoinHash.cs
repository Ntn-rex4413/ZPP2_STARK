using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.DatabaseModel
{
    public class CoinHash : EqualityComparer<Cryptocurrency>
    {
        public override bool Equals(Cryptocurrency x, Cryptocurrency y)
        {
            return x.Symbol.Equals(y.Symbol);
        }

        public override int GetHashCode([DisallowNull] Cryptocurrency obj)
        {
            return base.GetHashCode();
        }
    }
}
