using LocatorsForWebElements.PageObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace LocatorsForWebElements
{
    public class Locators_For_Web_Elements
    {
        private readonly ChromeOptions _options = new ChromeOptions();
        private IWebDriver _driver = null!;
        private MainPage _mainPage = null!;
        private CareersPage _careersPage = null!;
        private JobSearchPage _jobSearchPage = null!;
        private SearchResultPage _searchResultPage = null!;
        public Locators_For_Web_Elements()
        {
            _options.AddArgument("--start-maximized");
            _options.AddArgument("--incognito");
        }
        private void InitializeChromeWebDriver()
        {
            _driver = new ChromeDriver(_options);
        }

        [Theory]
        [InlineData("C#", "Poland")]
        [InlineData("Java", "Ukraine")]
        public void Task1ValidateThatUserCanSearchForaPositionBasedOnCriteria(string programminglanguage, string country)
        {
            try
            {
                InitializeChromeWebDriver();
                NavigateToMainPage();
                NavigateToCareersPage();
                NavigateToJobSearchPage();
                _jobSearchPage.EnterProgrammingLanguageIntoSearchField(programminglanguage);
                _jobSearchPage.SelectCountry(country);
                _jobSearchPage.ClickRemotePositionRadioButton();
                _jobSearchPage.ClickSearchButton();
                Assert.Contains(programminglanguage, _jobSearchPage.FindRelevantJobOffer(programminglanguage).Text);
                _driver.Quit();
            }
            catch (Exception)
            {
                _driver.Quit();
                throw;
            }

        }
        private void NavigateToMainPage()
        {
            _mainPage = new MainPage(_driver);
        }
        private void NavigateToCareersPage()
        {
            _careersPage = _mainPage.ClickCareersLink();
        }
        private void NavigateToJobSearchPage()
        {
            _jobSearchPage = _careersPage.ClickJobSearchPageButton();
        }

        [Theory]
        [InlineData("BLOCKCHAIN")]
        [InlineData("Cloud")]
        [InlineData("Automation")]
        public void Task2ValidateGlobalSearchWorksAsExpected(string searchQuery)
        {
            //* PartialLinkText
            try
            {
                InitializeChromeWebDriver();
                NavigateToMainPage();
                _mainPage.ClickSearchButton();
                _mainPage.EnterSearchQuery(searchQuery);
                ClickFindButton();
                Assert.Equal(ResultsThatContainSearchKeyWord(searchQuery, _searchResultPage.GetSearchResults()).Count(), _searchResultPage.GetSearchResults().Count);
                _driver.Quit();
            }
            catch (Exception)
            {
                _driver.Quit();
                throw;
            }
        }
        private void ClickFindButton()
        {
            _searchResultPage = _mainPage.ClickSubmitSearchButton();
        }
        private static IEnumerable<IWebElement> ResultsThatContainSearchKeyWord(string searchQuery, ReadOnlyCollection<IWebElement> elements)
        {
            IEnumerable<IWebElement> filteredElements =
            from element in elements
            where element.Text.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
            select element;
            return filteredElements;
        }
        [Theory]
        [InlineData("Code-Of-Conduct_01_26.pdf")]
        public void Task3ValidateDownloadFunctionWorksAsExpected(string nameOfFile)
        {
            try
            {
                InitializeChromeWebDriver();
                NavigateToMainPage();
                _mainPage.ScrollToFooter();
                _mainPage.ClickCodeOfConductLink();
                Assert.True(IsFileDownloaded(nameOfFile));
                _driver.Quit();
            }
            catch (Exception)
            {
                _driver.Quit();
                throw;
            }
        }
        public bool IsFileDownloaded(string fileName)
        {
            var downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            var filePath = Path.Combine(downloadPath, fileName);
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            return wait.Until(d =>
            {
                try
                {
                    return File.Exists(filePath);
                }
                catch (Exception ex)
                {
                    if (ex is IOException || ex is UnauthorizedAccessException)
                    {
                        return false;
                    }
                    else throw;
                }
            });
        }
        //    _driver.Navigate().GoToUrl("chrome://downloads/");
        //    var _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3))
        //    {
        //        PollingInterval = TimeSpan.FromMilliseconds(500)
        //    };
        //    _wait.Until(d =>
        //    {
        //        try
        //        {
        //            var shadowRoot = _driver.FindElement(By.CssSelector("body > downloads-manager"))
        //                .GetShadowRoot().FindElement(By.CssSelector("#list>"))
        //                .GetShadowRoot().FindElement(By.CssSelector("#fileLink"));
        //            return shadowRoot.Text.Contains(fileName);
        //        }
        //        catch (Exception ex)
        //        {
        //            if (ex is StaleElementReferenceException || ex is NoSuchElementException)
        //            {
        //                return false;
        //            }
        //            else throw;
        //        }
        //    });
        //    return true;
        //}
    }
}