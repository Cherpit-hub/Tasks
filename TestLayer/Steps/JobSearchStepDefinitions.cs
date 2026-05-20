using BusinessLayer.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using System;

namespace TestLayer.Steps
{
    [Binding]
    public class JobSearchStepDefinitions
    {
        private IWebDriver _driver;
        private MainPage _mainPage;
        private CareersPage _careersPage = null!;
        private JobSearchPage _jobSearchPage = null!;

        public JobSearchStepDefinitions(ScenarioContext scenarioContext)
        {
            _driver = (IWebDriver)scenarioContext["WebDriver"];
            _mainPage = (MainPage)scenarioContext["MainPage"];
        }

        [Given("User clicks on 'Careers' link")]
        public void GivenUserClicksOnCareersLink()
        {
            ClickCareersLink();
        }

        [Then("User clicks 'Start your search here' button on 'Careers' page")]
        public void ThenUserClicksStartYourSearchHereButtonOnCareersPage()
        {
            NavigateToJobSearchPage();
        }

        [When("User enters {string} into the 'search by role or keyword' field")]
        public void WhenUserEntersIntoTheSearchByRoleOrKeywordField(string programming_language)
        {
            _jobSearchPage.EnterProgrammingLanguageIntoSearchField(programming_language);
        }

        [When("User selects {string} in 'choose your country' field")]
        public void WhenUserSelectsInChooseYourCountryField(string country)
        {
            _jobSearchPage.SelectCountry(country);
        }

        [When("User selects 'Remote' option")]
        public void WhenUserSelectsRemoteOption()
        {
            _jobSearchPage.ClickRemotePositionRadioButton();
        }

        [When("User clicks 'Search' button")]
        public void WhenUserClicksSearchButton()
        {
            _jobSearchPage.ClickJobSearchButton();
        }

        [Then("The latest element of the list should contain {string}.")]
        public void ThenTheJobShouldHaveTheRequirementInIt_(string programming_language)
        {
            Assert.Contains(programming_language, _jobSearchPage.FindRelevantJobOffer(programming_language).Text);
        }

        private void ClickCareersLink()
        {
            _careersPage = _mainPage.ClickCareersLink();
        }
        private void NavigateToJobSearchPage()
        {
            _jobSearchPage = _careersPage.ClickJobSearchPageButton();
        }
    }
}
