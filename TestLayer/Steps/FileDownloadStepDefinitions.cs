using BusinessLayer.PageObjects;
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

        [Given("I scroll down to the footer")]
        public void GivenIScrollDownToTheFooter()
        {
            _mainPage.ScrollToFooter();
        }

        [When("I click on Code of Ethical conduct \\(PDF) in Policies section")]
        public void WhenIClickOnCodeOfEthicalConductPDFInPoliciesSection()
        {
            _mainPage.ClickCodeOfConductLink();
        }

        [Then("The file {string} should be downloaded")]
        public void ThenTheFileShouldBeDownloaded(string nameOfFile)
        {
            Assert.True(IsFileDownloaded(nameOfFile));
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
    }
}
