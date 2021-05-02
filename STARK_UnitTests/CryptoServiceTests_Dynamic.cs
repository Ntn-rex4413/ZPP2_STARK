using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STARK_Project.CryptoAPIService;

namespace STARK_UnitTests
{
    /// <summary>
    /// dynamic - will not pass unless you copy proper price from any cryptocurrency website
    /// </summary>
    [TestClass]
    public class CryptoServiceTests_Dynamic
    {
        /// <summary>
        /// get all cryptocurrencies and ensure that price is correct
        /// by JB
        /// </summary>
        [TestMethod]
        public void CryptocurrencySummary_ReturnsCorrectValue_IsTrue()
        {
            var service = new CryptoService();

            var data = service.GetCryptocurrenciesInfoAsync("USD").Result;

            Assert.AreEqual(Convert.ToSingle(58583.58), data.RAW["BTC"]["USD"].Price);
        }
        /// <summary>
        /// get one cryptocurrency and ensure that price is correct
        /// by JB
        /// </summary>
        [TestMethod]
        public void CryptocurrencyDetails_ReturnsCorrectValue_IsTrue()
        {
            var service = new CryptoService();

            var data = service.GetCryptocurrencyInfoAsync("BTC", "USD").Result;

            Debug.WriteLine(data.Price);

            Assert.AreEqual(Convert.ToSingle(58587.54), data.Price);
        }
    }
}
