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
        public void CryptocurrencySymbols_Returns200Values_IsTrue()
        {
            var enums = Enum.GetNames(typeof(CryptocurrencySymbols));

            Assert.AreEqual(200, enums.Length);
        }

        [TestMethod]
        public void CryptocurrencySummary_Returns200Values_IsTrue()
        {
            var service = new CryptoService();

            var data = service.GetCryptocurrenciesInfoAsync(Enum.Parse<CurrencySymbols>("PLN")).Result;
            var enums = Enum.GetNames(typeof(CryptocurrencySymbols));
            var result = new List<CryptoInfo>();
            foreach (var item in enums)
            {
                result.Add(data.RAW[Enum.Parse<CryptocurrencySymbols>(item)][CurrencySymbols.PLN]);
            }

            Assert.AreEqual(200, result.Count);
        }
    }
}
