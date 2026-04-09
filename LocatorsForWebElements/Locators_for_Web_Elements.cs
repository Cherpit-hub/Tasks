using LocatorsForWebElements.PageObjects;
using Microsoft.Extensions.Configuration;
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
        public void Task1ValidateThatUserCanSearchForaPositionBasedOnCriteria(string programminglanguage,string country)
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
    }
}