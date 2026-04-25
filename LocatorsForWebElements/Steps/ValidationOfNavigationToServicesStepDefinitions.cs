using LocatorsForWebElements.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using Reqnroll.Assist;
using System;

namespace LocatorsForWebElements.Steps
{
    [Binding]
    public class ValidationOfNavigationToServicesStepDefinitions
    {
        private readonly ChromeOptions _options = new ChromeOptions();
        private IWebDriver _driver = null!;
        private MainPage _mainPage = null!;
        private ServicesOptionPage _servicesOptionPage = null!;
        private string _services = null!;
        [BeforeScenario]
        public void TestSetup()
        {
            _options.AddArgument("--start-maximized");
            _options.AddArgument("--incognito");
            _driver = new ChromeDriver(_options);
        }
        [AfterScenario]
        public void TestTearDown()
        {
            _driver.Quit();
        }

        [Given("I navigate to the Epam website")]
        public void GivenINavigateToTheEpamWebsite()
        {
            NavigateToMainPage();
        }

        [When("I click on the Services link")]
        public void WhenIClickOnTheLink()
        {
            _mainPage.HoverOverServicesLink();
        }

        [When("Click on the {string} link")]
        public void WhenClickOnTheLink(string services)
        {
            _services = services;
            _servicesOptionPage = _mainPage.ClickOnServicesCategoryLink(services);
        }

        [Then("It should have the correct title")]
        public void ThenItShouldHaveTheCorrectTitle()
        {
            Assert.Equal(_services, _servicesOptionPage.GetTitle());
        }

        [Then("have section Our related Expertise displayed on the page")]
        public void ThenHaveSectionDisplayedOnThePage()
        {
            Assert.True(_servicesOptionPage.IsSectionDisplayed());
        }


        private void NavigateToMainPage()
        {
            _mainPage = new MainPage(_driver);
        }

    }
}
