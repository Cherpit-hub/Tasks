using LocatorsForWebElements.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using System;
using System.Collections.ObjectModel;

namespace LocatorsForWebElements.Steps
{
    [Binding]
    public class GlobalSearchStepDefinitions
    {

        private IWebDriver _driver;
        private MainPage _mainPage;
        private SearchResultPage _searchResultPage = null!;

        public GlobalSearchStepDefinitions(ScenarioContext scenarioContext)
        {
            _driver = (IWebDriver)scenarioContext["WebDriver"];
            _mainPage = (MainPage)scenarioContext["MainPage"];
        }

        [Given("I Click on magnifier icon")]
        public void GivenIClickOnMagnifierIcon()
        {
            _mainPage.ClickMagnifierButton();
        }

        [When("I fill the search field with {string}")]
        public void WhenIFillTheSearchFieldWith(string Query)
        {
            _mainPage.EnterSearchQuery(Query);
        }

        [When("Click Find button")]
        public void WhenClickFindButton()
        {
            ClickFindButton();
        }

        [Then("In the list of results all links should contain a {string} in the text")]    
        public void ThenInTheListOfResultsAllLinksShouldContainAInTheText(string Query)
        {
            Assert.Equal(SearchResultsThatContainQuery(Query), _searchResultPage.GetSearchResults().Count);
        }

        private void ClickFindButton()
        {
            _searchResultPage = _mainPage.ClickSubmitSearchButton();
        }
        private int SearchResultsThatContainQuery(string Query)
        {
            return ResultsThatContainSearchKeyWord(Query, _searchResultPage.GetSearchResults()).Count();
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
