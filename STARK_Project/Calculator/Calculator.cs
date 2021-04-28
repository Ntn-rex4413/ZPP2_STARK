using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STARK_Project.CryptoAPIService;
using STARK_Project.DBServices;

namespace STARK_Project.Calculator
{
    public class Calculator
    {
        private IDbService _dbService;
        private ICryptoService _cryptoService;
        public Calculator(IDbService dbService, ICryptoService cryptoService)
        {
            _dbService = dbService;
            _cryptoService = cryptoService;
        }
        public double Calculate(double valueLeft, string currencyLeft, string currencyRight)
        {
            bool isLeftCurrencyCrypto;
            bool isRightCurrencyCrypto;

            throw new NotImplementedException();
        }
    }

    public interface ICalculator
    {
        double Calculate(double valueLeft, string currencyLeft, string currencyRight);
    }
}
