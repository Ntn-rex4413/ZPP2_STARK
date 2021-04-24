using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace STARK_SeleniumTests
{
    public class DetailsTests
    {
        private IWebDriver webDriver;

        [SetUp]
        public void Setup()
        {
            webDriver = new FirefoxDriver();
            webDriver.Navigate().GoToUrl("https://localhost:44311/Details?cryptocurrency=BTC");
        }

        [Test]
        public void CryptocurrencyName_Displayed_IsTrue()
        {
            IWebElement label = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[1]/h1"));

            Assert.That(label.Displayed, Is.True);
        }

        [Test]
        public void CryptocurrencyLogo_Displayed_IsTrue()
        {
            IWebElement logo = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[1]/i"));

            Assert.That(logo.Displayed, Is.True);
        }

        [Test]
        public void CryptocurrencyPrice_Displayed_IsTrue()
        {
            IWebElement label = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[3]/div[1]/div/div/div/div[1]/div[2]"));

            Assert.That(label.Displayed, Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            if (webDriver != null)
            {
                webDriver.Dispose();
            }
        }
    }
}