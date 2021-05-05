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
    public class LoginPageTests
    {
        private IWebDriver webDriver;

        [SetUp]
        public void Setup()
        {
            webDriver = new FirefoxDriver();
            webDriver.Navigate().GoToUrl("https://localhost:44311/Identity/Account/Login");
        }

        [Test]
        public void LoginWelcome_Displayed_IsTrue()
        {
            IWebElement element = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div/div/div/div/div/div/div/div[1]/h1"));

            Assert.That(element.Displayed, Is.True);
        }

        [Test]
        public void LoginButton_Displayed_IsTrue()
        {
            IWebElement element = webDriver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div/div/div/div/div/div/div/form[1]/div[4]/button"));

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
