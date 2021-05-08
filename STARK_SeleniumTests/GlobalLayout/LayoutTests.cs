using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace STARK_SeleniumTests.GlobalLayout
{
    public class LayoutTests
    {
        private IWebDriver webDriver;

        [SetUp]
        public void Setup()
        {
            webDriver = new FirefoxDriver();
            webDriver.Navigate().GoToUrl("https://localhost:44311");
        }

        [Test]
        public void AlertDropDown_Displayed_IsTrue()
        {
            IWebElement element = webDriver.FindElement(By.XPath("//*[@id=\"alertsDropdown\"]"));

            Assert.That(element.Displayed, Is.True);
        }

        [Test]
        public void MessagesDropDown_Displayed_IsTrue()
        {
            IWebElement element = webDriver.FindElement(By.XPath("//*[@id=\"messagesDropdown\"]"));

            Assert.That(element.Displayed, Is.True);
        }

        [Test]
        public void RegisterLink_Displayed_IsTrue()
        {
            IWebElement element = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/nav/ul/ul/li[1]/a"));

            Assert.That(element.Displayed, Is.True);
        }

        [Test]
        public void LoginLink_Displayed_IsTrue()
        {
            IWebElement element = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/nav/ul/ul/li[2]/a"));

            Assert.That(element.Displayed, Is.True);
        }

        [Test]
        public void SidebarToggle_Displayed_IsTrue()
        {
            IWebElement element = webDriver.FindElement(By.XPath("//*[@id=\"sidebarToggle\"]"));

            Assert.That(element.Displayed, Is.True);
        }

        [Test]
        public void Logo_Displayed_IsTrue()
        {
            IWebElement element = webDriver.FindElement(By.XPath("/html/body/div[1]/ul/a/div[2]"));

            Assert.That(element.Displayed, Is.True);
        }

        [Test]
        public void SummaryLink_Displayed_IsTrue()
        {
            IWebElement element = webDriver.FindElement(By.XPath("/html/body/div[1]/ul/li[1]/a/span"));

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
