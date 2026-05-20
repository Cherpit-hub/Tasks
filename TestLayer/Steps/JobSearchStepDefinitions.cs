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
        private readonly ChromeOptions _options = new ChromeOptions();
        private IWebDriver _driver;
        private MainPage _mainPage;
        private CareersPage _careersPage = null!;
        private JobSearchPage _jobSearchPage = null!;

        public JobSearchStepDefinitions(ScenarioContext scenarioContext)
        {
            _driver = (IWebDriver)scenarioContext["WebDriver"];
            _mainPage = (MainPage)scenarioContext["MainPage"];
        }

        [Given("I click on Careers link")]
        public void GivenIClickOnCareersLink()
        {
            ClickCareersLink();
        }

        [When("T click Start your search here on Careers page")]
        public void WhenTClickStartYourSearchHereOnCareersPage()
        {
            NavigateToJobSearchPage();
        }

        [Then("Jobs page should load")]
        public void ThenJobsPageShouldLoad()
        {
            // Next steps have waits
        }

        [When("I Enter {string} into the Search by role or keyword field")]
        public void WhenIEnterIntoTheSearchByRoleOrKeywordField(string programming_language)
        {
            _jobSearchPage.EnterProgrammingLanguageIntoSearchField(programming_language);
        }

        [When("I Select {string} in Choose your country field")]
        public void WhenISelectInChooseYourCountryField(string country)
        {
            _jobSearchPage.SelectCountry(country);
        }

        [When("I Select Remote option")]
        public void WhenISelectRemoteOption()
        {
            _jobSearchPage.ClickRemotePositionRadioButton();
        }

        [When("Click Search button")]
        public void WhenClickSearchButton()
        {
            _jobSearchPage.ClickSearchButton();
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
