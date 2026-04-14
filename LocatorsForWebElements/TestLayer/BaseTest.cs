using LocatorsForWebElements.BusinessLayer.PageObjects;
using LocatorsForWebElements.CoreLayer;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
using OpenQA.Selenium;
using static LocatorsForWebElements.CoreLayer.WebDriverFactory;

namespace LocatorsForWebElements.TestLayer
{
    public abstract class BaseTest : IDisposable
    {
        public ILog Log
        {
            get { return LogManager.GetLogger(this.GetType()); }
        }
        public IWebDriver _driver;
        public MainPage _mainPage = null!;
        protected BaseTest()
        {
            XmlConfigurator.Configure(new FileInfo("Log.config"));
            var browserType = (BrowserType)Enum.Parse(typeof(BrowserType), Configuration.BrowserType);
            _driver = WebDriverFactory.CreateWebDriver(browserType);
        }

        public void Dispose()
        {
            _driver.Quit();
            GC.SuppressFinalize(this);
        }
    }
}
