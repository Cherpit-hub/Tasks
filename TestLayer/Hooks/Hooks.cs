using BusinessLayer.PageObjects;
using CoreLayer;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CoreLayer.Logger;
using static CoreLayer.WebDriverFactory;

namespace TestLayer.Hooks
{
    [Binding]
    internal class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver = null!;
        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            XmlConfigurator.Configure(new FileInfo("Log.config"));
        }
        [BeforeScenario]
        public void TestSetup()
        {
            _driver = CreateWebDriver(Configuration.EnvBrowser ?? Configuration.BrowserType);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            _driver.Manage().Window.Maximize();
            _scenarioContext["WebDriver"] = _driver;
            _scenarioContext["MainPage"] = new MainPage(_driver);
            SetLogLevel("DEBUG");
        }
        [AfterScenario]
        public void TestTearDown()
        {
            _driver.Quit();
        }
        public static void SetLogLevel(string level)
        {
            // Get the root logger (or specific logger if needed)
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            var root = hierarchy.Root;

            // Set the log level based on the input string
            switch (level.ToUpper())
            {
                case "DEBUG":
                    root.Level = log4net.Core.Level.Debug;
                    break;
                case "WARN":
                    root.Level = log4net.Core.Level.Warn;
                    break;
                default:
                    Log.Warn("Unknown log level: " + level);
                    break;
            }
        }
    }
}
