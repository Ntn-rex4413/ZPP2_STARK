using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace STARK_SeleniumTests
{
    public class SummaryTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CryptocurrencyName_CorrectlyDisplayed_IsTrue()
        {

            IWebDriver webDriver = new FirefoxDriver();

            webDriver.Navigate().GoToUrl("https://localhost:44311/");

            IWebElement aurLabel = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[11]/div/div/div[1]/h6"));

            Assert.That(aurLabel.Displayed, Is.True);
        }

        [Test]
        public void UpdateButton_CorrectlyDisplayed_IsTrue()
        {

            IWebDriver webDriver = new FirefoxDriver();

            webDriver.Navigate().GoToUrl("https://localhost:44311/");

            IWebElement aurLabel = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[3]/div/div/div[2]/div/a[4]"));

            Assert.That(aurLabel.Displayed, Is.True);
        }

        [Test]
        public void SubscribeButton_CorrectlyDisplayed_IsTrue()
        {

            IWebDriver webDriver = new FirefoxDriver();

            webDriver.Navigate().GoToUrl("https://localhost:44311/");

            IWebElement aurLabel = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div/a[3]"));

            Assert.That(aurLabel.Displayed, Is.True);
        }

        [Test]
        public void BuySellButton_CorrectlyDisplayed_IsTrue()
        {

            IWebDriver webDriver = new FirefoxDriver();

            webDriver.Navigate().GoToUrl("https://localhost:44311/");

            IWebElement aurLabel = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div/a[2]"));

            Assert.That(aurLabel.Displayed, Is.True);
        }


        [Test]
        public void DetailsButton_CorrectlyDisplayed_IsTrue()
        {

            IWebDriver webDriver = new FirefoxDriver();

            webDriver.Navigate().GoToUrl("https://localhost:44311/");

            IWebElement aurLabel = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div/a[1]"));

            Assert.That(aurLabel.Displayed, Is.True);
        }
    }
}