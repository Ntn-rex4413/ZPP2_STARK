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
        /// <summary>
        /// calculate value of currency based on other currency
        /// </summary>
        /// <param name="valueLeft"></param>
        /// <param name="currencyLeft"></param>
        /// <param name="currencyRight"></param>
        /// <returns></returns>
        public double Calculate(double valueLeft, string currencyLeft, string currencyRight)
        {
            bool isLeftCurrencyCrypto = false;
            bool isRightCurrencyCrypto = false;

            double valueRight = double.MinValue;

            var cryptocurrencies = _cryptoService.GetCryptocurrenciesAsync().Result;
            foreach (var item in cryptocurrencies)
            {
                if (item.Key == currencyLeft) isLeftCurrencyCrypto = true;
                if (item.Key == currencyRight) isRightCurrencyCrypto = true;
            }

            switch (isLeftCurrencyCrypto, isRightCurrencyCrypto)
            {
                case (true, true):
                {
                    break;
                }
                case (true, false):
                {
                    break;
                }
                case (false, false):
                {
                    break;
                }
                case (false, true):
                {
                    break;
                }
            }

            return valueRight;
        }
    }

    public interface ICalculator
    {
        double Calculate(double valueLeft, string currencyLeft, string currencyRight);
    }
}
