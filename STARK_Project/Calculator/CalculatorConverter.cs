using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STARK_Project.CryptoAPIService;
using STARK_Project.DBServices;

namespace STARK_Project.Calculator
{
    public class CalculatorConverter
    {
        private IDbService _dbService;
        private ICryptoService _cryptoService;
        public CalculatorConverter(IDbService dbService, ICryptoService cryptoService)
        {
            _dbService = dbService;
            _cryptoService = cryptoService;
        }
        /// <summary>
        /// calculate value of currency based on other currency, convert
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

            //check which currency is crypto
            var cryptocurrencies = _cryptoService.GetCryptocurrenciesAsync().Result;
            foreach (var item in cryptocurrencies)
            {
                if (item.Key == currencyLeft) isLeftCurrencyCrypto = true;
                if (item.Key == currencyRight) isRightCurrencyCrypto = true;
            }

            //calculate based on currency type
            switch (isLeftCurrencyCrypto, isRightCurrencyCrypto)
            {
                case (true, true):
                {
                    var tempValue = "PLN";
                    var currencyLeftInfo = _cryptoService.GetCryptocurrencyInfoAsync(currencyLeft, tempValue).Result;
                    var currencyRightInfo = _cryptoService.GetCryptocurrencyInfoAsync(currencyRight, tempValue).Result;
                    
                    break;
                }
                case (true, false):
                {
                    var cryptocurrencyLeftInfo = _cryptoService.GetCryptocurrencyInfoAsync(currencyLeft, currencyRight).Result;
                    valueRight = cryptocurrencyLeftInfo.Price * valueLeft;
                    break;
                }
                case (false, false):
                {
                    var tempValue = "ETH";
                    var currencyLeftInfo = _cryptoService.GetCryptocurrencyInfoAsync(tempValue, currencyLeft).Result;
                    var currencyRightInfo = _cryptoService.GetCryptocurrencyInfoAsync(tempValue, currencyRight).Result;

                    break;
                }
                case (false, true):
                {
                    var cryptocurrencyRightInfo =
                        _cryptoService.GetCryptocurrencyInfoAsync(currencyRight, currencyLeft).Result;
                    valueRight = cryptocurrencyRightInfo.Price * valueLeft;
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
