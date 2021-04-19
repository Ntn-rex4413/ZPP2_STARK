using STARK_Project.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.DBServices
{
    interface IDbService
    {
        bool AddCryptocurrencyToDatabaseAsync(Cryptocurreny);
    }
}
