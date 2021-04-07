using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace STARK_SeleniumTests.Pages
{
    public class SummaryPage
    {
        private IWebDriver _driver { get; }
        public SummaryPage(IWebDriver webDriver)
        {
            _driver = webDriver;
        }

        public IWebElement lnkDetails => _driver.FindElement(By.LinkText("Details"));
        public IWebElement lblDetails => _driver.FindElement(By.LinkText("Zmiana w $"));

        public void ClickDetails()
        {
            lnkDetails.Click();
        }

        public bool IsLabelVisible() => lblDetails.Displayed;
    }
}
