using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STARK_Project.CryptoApiModel;
using STARK_Project.CryptoAPIService;

namespace STARK_UnitTests
{
    [TestClass]
    public class CryptoServiceTests
    {
        /// <summary>
        /// by JB
        /// </summary>
        [TestMethod]
        public void CryptocurrencySummary_ReturnsAllValues_IsTrue()
        {
            var service = new CryptoService();

            var data = service.GetCryptocurrenciesInfoAsync("PLN").Result;

            Assert.AreEqual(29, data.RAW.Count);
        }

        /// <summary>
        /// by JB
        /// </summary>
        [TestMethod]
        public void CryptocurrenciesNames_ContainsAllValues_IsTrue()
        {
            var service = new CryptoService();

            var data = service.GetCryptocurrenciesAsync().Result;

            Assert.AreEqual(6577, data.Count);
        }

        /// <summary>
        /// by NJ
        /// </summary>
        [TestMethod]
        public void CryptocurrencySummary_ContainsNoDuplicates_IsTrue()
        {
            var service = new CryptoService();

            var data = service.GetCryptocurrenciesInfoAsync("PLN").Result;

            Assert.IsNotNull(data);
            Assert.AreNotEqual(0, data.RAW.Count);
            Assert.AreEqual(data.RAW.Count, data.RAW.Distinct().Count());
        }
    }
}
