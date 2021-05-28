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
        /// <summary>
        /// by MG
        /// </summary>
        [TestMethod]
        public void TopCryptocurrenciesNames_GetsAmount_IsTrue()
        {
            int amount = 10;
            var service = new CryptoService();

            var data = service.GetRankingDataAsync(amount, "PLN").Result;

            Assert.IsNotNull(data);
            Assert.AreEqual(amount, data.Count());
        }

        [TestMethod]
        public void GetHistoricalData_ConnectionAndData_isValid()
        {
            var service = new CryptoService();
            var symbol = "BTC";
            var currency = "PLN";

            var dataDaily =  service.GetHistoricalData(HistoricalDataTypes.Daily, symbol, currency,null, null).Result;
            var dataHourly =  service.GetHistoricalData(HistoricalDataTypes.Hourly, symbol, currency,null, null).Result;
            var dataMinute =  service.GetHistoricalData(HistoricalDataTypes.Minute, symbol, currency,null, null).Result;

            Assert.IsNotNull(dataDaily);
            Assert.IsNotNull(dataHourly);
            Assert.IsNotNull(dataMinute);

            Assert.IsTrue(dataDaily.Data.Length > 0);
            Assert.IsTrue(dataHourly.Data.Length > 0);
            Assert.IsTrue(dataMinute.Data.Length > 0);

        }

        [TestMethod]
        public void GetHistoricalData_limit_isValid()
        {
            var service = new CryptoService();
            var symbol = "BTC";
            var currency = "PLN";

            var dataDaily = service.GetHistoricalData(HistoricalDataTypes.Daily, symbol, currency, 10, null).Result;
            var dataHourly = service.GetHistoricalData(HistoricalDataTypes.Hourly, symbol, currency, 12, null).Result;
            var dataMinute = service.GetHistoricalData(HistoricalDataTypes.Minute, symbol, currency, 25, null).Result;

            Assert.IsNotNull(dataDaily);
            Assert.IsNotNull(dataHourly);
            Assert.IsNotNull(dataMinute);

            Assert.IsTrue(dataDaily.Data.Length == 11);
            Assert.IsTrue(dataHourly.Data.Length == 13);
            Assert.IsTrue(dataMinute.Data.Length == 26);

        }
    }
}
