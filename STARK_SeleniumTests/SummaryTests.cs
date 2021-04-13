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

        [Test]
        public void AUR_CorrectlyDisplayed_IsTrue()
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

    }
}