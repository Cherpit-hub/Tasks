using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace BusinessLayer.PageObjects
{
    public class BasePage
    {
        readonly By _servicesLocator = By.LinkText("Services");
        readonly By _careersLocator = By.LinkText("Careers");
        readonly By _magnifiericonLocator = By.ClassName("header__icon");
        readonly By _searchFieldLocator = By.Id("new_form_search");
        readonly By _searchButtonLocator = By.XPath("//button[contains(@class,'custom-button')]");
        protected readonly IWebDriver _driver;
        protected readonly WebDriverWait _wait;
        public static log4net.ILog Log => CoreLayer.Logger.Log;

        protected BasePage(IWebDriver driver)

        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(7))
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
            Log.Info("Clicking Global search button");
            _wait.Until((d =>
            {
                try
                {
                    FindElement(_magnifiericonLocator).Click();
                    return FindElement(_searchFieldLocator).Displayed;
                }
                catch (Exception ex)
                {
                    if (ex is StaleElementReferenceException || ex is ElementClickInterceptedException || ex is ElementNotInteractableException)
                    {
                        Log.Warn($"Encountered {ex.GetType().Name} while waiting for Search field to be Displayed. Retrying...");
                        return false;
                    }
                    else throw;
                }
            }));
            
        }
        public void EnterSearchQuery(string searchQuery)
        {
            Log.Info($"Entering search query: {searchQuery}");
            _wait.Until((d=> FindElement(_searchFieldLocator).Enabled));
            FindElement(_searchFieldLocator).SendKeys(searchQuery);
        }
        public SearchResultPage ClickSubmitSearchButton()
        {
            Log.Info("Clicking submit search button");
            _wait.Until((d => FindElement(_searchButtonLocator).Enabled));
            FindElement(_searchButtonLocator).Click();
            Log.Info("Search button clicked successfully");
            return new SearchResultPage(_driver);
        }
        public CareersPage ClickCareersLink()
        {
            Log.Info("Clicking Careers button");
            FindElement(_careersLocator).Click();
            Log.Info("Careers button clicked successfully");
            return new CareersPage(_driver);
        }
        public void HoverOverServicesLink()
        {
            var servicesElement = FindElement(_servicesLocator);
            var actions = new Actions(_driver);
            actions.MoveToElement(servicesElement).Perform();
        }
        public ServicesOptionPage ClickOnServicesCategoryLink(string linkText)
        {
            FindElement(By.LinkText(linkText)).Click();
            return new ServicesOptionPage(_driver);
        }
    }
}
