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
        public void DetailsButtonRedirectsCorrectlyIsTrue()
        {
            //open browser
            IWebDriver webDriver = new FirefoxDriver();
            //navigate to site
            webDriver.Navigate().GoToUrl("https://localhost:44311/");

            //identify details button
            IWebElement detailsLink = webDriver.FindElement(By.XPath("//*[contains(., 'Details')]"));

            //operation
            detailsLink.Click();

            var detailsLabel = webDriver.FindElement(By.XPath("//div[text() = 'Zmiana w $'"));

            //assertion
            Assert.That(detailsLabel.Displayed, Is.True);
        }

    }
}