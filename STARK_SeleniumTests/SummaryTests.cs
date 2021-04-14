using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace STARK_SeleniumTests
{
    public class SummaryTests
    {
        private IWebDriver webDriver;

        [SetUp]
        public void Setup()
        {
            webDriver = new FirefoxDriver();
            webDriver.Navigate().GoToUrl("https://localhost:44311/");
        }

        [Test]
        public void CryptocurrencyName_Displayed_IsTrue()
        {
            IWebElement label = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[1]/h6"));

            Assert.That(label.Displayed, Is.True);
        }

        [Test]
        public void SubscribeButton_Displayed_IsTrue()
        {
            IWebElement button = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div/a[3]"));

            Assert.That(button.Displayed, Is.True);
        }

        [Test]
        public void BuySellButton_Displayed_IsTrue()
        {
            IWebElement button = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div/a[2]"));

            Assert.That(button.Displayed, Is.True);
        }


        [Test]
        public void DetailsButton_Displayed_IsTrue()
        {
            IWebElement button = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div/a[1]"));

            Assert.That(button.Displayed, Is.True);
        }
    }
}