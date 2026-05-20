using BusinessLayer.PageObjects;
using CoreLayer;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Reqnroll;
using System;

namespace TestLayer.Steps
{
    [Binding]
    public class FileDownloadStepDefinitions
    {
        private IWebDriver _driver;
        private MainPage _mainPage;

        public FileDownloadStepDefinitions(ScenarioContext scenarioContext)
        {
            _driver = (IWebDriver)scenarioContext["WebDriver"];
            _mainPage = (MainPage)scenarioContext["MainPage"];
        }

        [Given("User scrolls down to the footer")]
        public void GivenUserScrollsDownToTheFooter()
        {
            _mainPage.ScrollToFooter();
        }

        [When("User clicks on 'Code of Ethical conduct' in policies section")]
        public void WhenUserClicksOnCodeOfEthicalConductPDFInPoliciesSection()
        {
            _mainPage.ClickCodeOfConductLink();
        }

        [Then("Verify that the file {string} is downloaded")]
        public void ThenVerifyThatTheFileIsDownloaded(string nameOfFile)
        {
            Assert.True(BrowserUtils.IsFileDownloaded(nameOfFile, _driver));
        }
    }
}
