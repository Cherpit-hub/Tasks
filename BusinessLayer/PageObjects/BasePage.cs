using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BusinessLayer.PageObjects
{
    public class BasePage
    {
        readonly By _magnifiericonLocator = By.ClassName("header__icon");
        readonly By _searchFieldLocator = By.Id("new_form_search");
        readonly By _searchButtonLocator = By.XPath("//button[contains(@class,'custom-button')]");
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
        public void ClickSearchButton()
        {
            FindElement(_magnifiericonLocator).Click();
        }
        public void EnterSearchQuery(string searchQuery)
        {
            FindElement(_searchFieldLocator).SendKeys(searchQuery);
        }
        public SearchResultPage ClickSubmitSearchButton()
        {
            _wait.Until(d =>
            {
                try
                {
                    FindElement(_searchButtonLocator).Click();
                    return ElementNotDisplayed(_searchButtonLocator);
                }
                catch (Exception ex)
                {
                    if (ex is StaleElementReferenceException || ex is NoSuchElementException || ex is ElementNotInteractableException)
                    {
                        return false;
                    }
                    else throw;
                }
            });
            return new SearchResultPage(_driver);
        }
        private bool ElementNotDisplayed(By locator)
        {
            return !FindElement(locator).Displayed;
        }
    }
}
