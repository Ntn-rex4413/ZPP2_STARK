using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace STARK_SeleniumTests
{
    public class SubscriptionsTests
    {
        private IWebDriver webDriver;

        [SetUp]
        public void Setup()
        {
            webDriver = new FirefoxDriver();
            webDriver.Navigate().GoToUrl("https://localhost:44311/Subscriptions");
        }

        [Test]
        public void CryptocurrencyName_Displayed_IsTrue()
        {
            IWebElement element = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[3]/div/div/div[1]/h6[1]"));

            Assert.That(element.Displayed, Is.True);
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
