using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STARK_Project.CryptoApiModel;
using STARK_Project.CryptoApiModel.CurrencyEnums;
using STARK_Project.CryptoApiModel.CurrencySymbolsEnums;
using STARK_Project.CryptoAPIService;

namespace STARK_UnitTests
{
    [TestClass]
    public class CryptoServiceTests
    {
        [TestMethod]
        public void CryptocurrencySummary_ReturnsAllValues_IsTrue()
        {
            var service = new CryptoService();

            var data = service.GetCryptocurrenciesInfoAsync("PLN").Result;

            Assert.AreEqual(26, data.RAW.Count);
        }
    }
}
