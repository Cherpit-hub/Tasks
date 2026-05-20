using BusinessLayer.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using Reqnroll.Assist;
using System;

namespace TestLayer.Steps
{
    [Binding]
    public class ValidationOfNavigationToServicesStepDefinitions
    {
        private IWebDriver _driver;
        private MainPage _mainPage;
        private ServicesOptionPage _servicesOptionPage = null!;
        private string _services = null!;
        public ValidationOfNavigationToServicesStepDefinitions(ScenarioContext scenarioContext) 
        {
            _driver = (IWebDriver)scenarioContext["WebDriver"];
            _mainPage = (MainPage)scenarioContext["MainPage"];

        }

        [Given("User is on the homepage")]
        public void GivenUserisOnTheHomepage()
        {
            //Hooks put us on a homepage
        }

        [When("User clicks on 'Services' link")]
        public void WhenUserClicksOnServicesLink()
        {
            _mainPage.HoverOverServicesLink();
        }

        [When("User clicks on the {string} link")]
        public void WhenUserClicksOnTheLink(string services)
        {
            _services = services;
            _servicesOptionPage = _mainPage.ClickOnServicesCategoryLink(services);
        }

        [Then("It should have the correct title")]
        public void ThenItShouldHaveTheCorrectTitle()
        {
            Assert.Equal(_services, _servicesOptionPage.GetTitle());
        }

        [Then("Have section 'Our Related Expertise' displayed on the page")]
        public void ThenHaveSectionDisplayedOnThePage()
        {
            Assert.True(_servicesOptionPage.IsSectionDisplayed());
        }

    }
}
