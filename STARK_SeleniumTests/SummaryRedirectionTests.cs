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
    class SummaryRedirectionTests
    {
        private IWebDriver webDriver;
        [SetUp]
        public void Setup()
        {
            webDriver = new FirefoxDriver();
            webDriver.Navigate().GoToUrl("https://localhost:44311/");
        }

        [Test]
        public void DetailsButton_RedirectsCorrectly_IsTrue()
        {
            IWebElement detailsLink = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div/a[1]"));

            detailsLink.Click();

            var detailsLabel = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[1]/h1")).Text;

            Assert.AreEqual("BTC", detailsLabel);
        }
    }
}
