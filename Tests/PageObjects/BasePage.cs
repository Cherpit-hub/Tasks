using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.PageObjects
{
    internal class BasePage
    {
        protected readonly IWebDriver _driver;
        protected readonly WebDriverWait _wait;
        protected BasePage(IWebDriver driver)

        {

            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5))
            {
                PollingInterval = TimeSpan.FromMilliseconds(1000)
            };
        }

        protected IWebElement FindElement(By locator)

        {

            return _driver.FindElement(locator);

        }

        public void NavigateTo(string url)

        {

            _driver.Navigate().GoToUrl(url);

        }
        public void ScrollToElement(By locator)
        {
            var element = FindElement(locator);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);
        }
    }
}
