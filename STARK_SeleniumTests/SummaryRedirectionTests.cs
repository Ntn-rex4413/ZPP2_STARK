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
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void DetailsButton_RedirectsCorrectly_IsTrue()
        {
            //open browser
            IWebDriver webDriver = new FirefoxDriver();
            //navigate to site
            webDriver.Navigate().GoToUrl("https://localhost:44311/");

            //identify details button
            IWebElement detailsLink = webDriver.FindElement(By.CssSelector("[href*='Details?cryptocurrency=BTC']"));

            //operation
            detailsLink.Click();

            //var detailsLabel = webDriver.FindElement(By.XPath("//div[contains(text(),'Change % Daily')]"));
            var detailsLabel = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[1]/h1"));

            //assertion
            Assert.That(detailsLabel.Displayed, Is.True);
        }
    }
}
