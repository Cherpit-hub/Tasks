using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace LocatorsForWebElements
{
    public class Locators_For_Web_Elements
    {
        private readonly string _url;
        private readonly ChromeOptions _options = new ChromeOptions();
        public Locators_For_Web_Elements()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            _url = config["ApplicationUrl"] ?? string.Empty;
            _options.AddArgument("--start-maximized");
            _options.AddArgument("--incognito");
        }
        [Theory]
        [InlineData("C#", "Poland")]
        [InlineData("Java", "Ukraine")]
        public void Task1ValidateThatUserCanSearchForaPositionBasedOnCriteria(string programminglanguage,string country)
        {   
            WebDriver driver = new ChromeDriver(_options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            var _careersLocator = By.LinkText("Careers");
            var _StartButtonLocator = By.ClassName("pinned-button-ui-23");
            var _SearchFieldLocator = By.Name("search");
            var _CountryFieldLocator = By.XPath("//div[@data-testid = 'country-dropdown']//input[contains(@class, 'input')]");
            var _CountryFieldCleanerLocator = By.XPath("//div[contains(@class,'clear-indicator')]");
            var _CountryOptionLocator = By.XPath($"//div[@role='listbox']//child::*[contains(text(), '{country}')]");
            var _RadioButtonRemoteLocator = By.CssSelector("label[for ='checkbox-vacancy_type-Remote-_r_0_']");
            var _SearchButtonLocator = By.XPath("//button[@type='submit' and contains(@name,'submit_search_box')]");
            var _ResultLocator = By.XPath("//div[contains(@data-testid,'accordion-section-container')]");
            var _ResultExtendedLocator = By.XPath($"//div[contains(@data-testid, 'categories-container')]//descendant::div[contains(text(),'{programminglanguage}')]");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3))
            {
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            try
            {
                driver.Navigate().GoToUrl(_url);
                driver.FindElement(_careersLocator).Click();
                driver.FindElement(_StartButtonLocator).Click();
                driver.FindElement(_SearchFieldLocator).SendKeys(programminglanguage);
                driver.FindElement(_CountryFieldCleanerLocator).Click();
                var countryfield = driver.FindElement(_CountryFieldLocator);
                wait.Until(d =>
                {
                    try
                    {
                        countryfield.Click();
                        var element = driver.FindElement(_CountryOptionLocator);
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'start'});", element);
                        element.Click();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        if (ex is StaleElementReferenceException || ex is ElementClickInterceptedException)
                        {
                            return false;
                        }
                        else throw;
                    }
                });
                driver.FindElement(_RadioButtonRemoteLocator).Click();
                driver.FindElement(_SearchButtonLocator).Click();
                IWebElement revealed = driver.FindElement(_ResultExtendedLocator);
                wait.Until(d =>
                {
                    try
                    {
                        driver.FindElement(_ResultLocator).Click();
                        return revealed.Displayed;
                    }
                    catch (Exception ex)
                    {
                        if (ex is StaleElementReferenceException || ex is ElementClickInterceptedException)
                        {
                            return false;
                        }
                        else throw;
                    }
                });
                Assert.Contains(programminglanguage, revealed.Text);
                driver.Quit();
            }
            catch (Exception)
            {
                driver.Quit();
                throw;
            }

            }
        [Theory]
        [InlineData("BLOCKCHAIN")]
        [InlineData("Cloud")]
        [InlineData("Automation")]
        public void Task2ValidateGlobalSearchWorksAsExpected(string searchQuery)
        {
            WebDriver driver = new ChromeDriver(_options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            var _magnifiericonLocator = By.ClassName("header__icon");
            var _searchFieldLocator = By.Id("new_form_search");
            var _searchButtonLocator = By.XPath("//button[contains(@class,'custom-button')]");
            var _articleLocator = By.TagName("article");
            //* PartialLinkText
            try
            {
                driver.Navigate().GoToUrl(_url);
                driver.FindElement(_magnifiericonLocator).Click();
                driver.FindElement(_searchFieldLocator).SendKeys(searchQuery);
                driver.FindElement(_searchButtonLocator).Click();
                var elements = driver.FindElements(_articleLocator);
                IEnumerable<IWebElement> filteredElements = 
                    from element in elements
                    where element.Text.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                    select element;
                Assert.Equal(elements.Count, filteredElements.Count());
                driver.Quit();
            }
            catch (Exception)
            {
                driver.Quit();
                throw;
            }
        }
    }
}