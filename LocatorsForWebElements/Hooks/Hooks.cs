using LocatorsForWebElements.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsForWebElements.Hooks
{
    [Binding]
    internal class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver = null!;
        private readonly ChromeOptions _options = new ChromeOptions();
        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void TestSetup()
        {
            _options.AddArgument("--start-maximized");
            _options.AddArgument("--incognito");
            _driver = new ChromeDriver(_options);
            _scenarioContext["WebDriver"] = _driver;
            _scenarioContext["MainPage"] = new MainPage(_driver);
        }
        [AfterScenario]
        public void TestTearDown()
        {
            _driver.Quit();
        }
    }
}
