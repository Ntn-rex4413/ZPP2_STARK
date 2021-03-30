using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STARK_Project.CryptoApiModel.CurrencyEnums;
using STARK_Project.CryptoApiModel.CurrencySymbolsEnums;
using STARK_Project.CryptoAPIService;

namespace STARK_UnitTests
{
    [TestClass]
    public class CryptoServiceTests
    {
        /// <summary>
        /// gets all cryptocurrencies
        /// </summary>
        [TestMethod]
        public void CryptocurrencySummary_Returns200_IsTrue()
        {
            var service = new CryptoService();

            var data = service.GetCryptocurrenciesInfoAsync(CurrencySymbols.PLN).Result;

            Assert.Equals(data, 200);
        }
        /// <summary>
        /// gets one cryptocurrency
        /// </summary>
        [TestMethod]
        public void CryptocurrencyDetails_CorrectData_IsTrue()
        {
            var service = new CryptoService();

            var data = service.GetCryptocurrencyInfoAsync(CryptocurrencySymbols.AAVE, CurrencySymbols.PLN).Result;

            Assert.Equals(data.Price, 234697.7557);
        }
    }
}
