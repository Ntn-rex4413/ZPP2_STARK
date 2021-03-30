using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STARK_Project.CryptoApiModel.CurrencyEnums;
using STARK_Project.CryptoAPIService;

namespace STARK_UnitTests
{
    [TestClass]
    public class CryptoServiceTests
    {
        [TestMethod]
        public void CryptocurrencySummary_Returns200_IsTrue()
        {
            var service = new CryptoService();

            var data = service.GetCryptocurrenciesInfoAsync(CurrencySymbols.PLN).Result;

            Assert.Equals(data, 200);
        }
    }
}
