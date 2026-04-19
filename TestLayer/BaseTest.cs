using BusinessLayer.PageObjects;
using CoreLayer;
using log4net;
using log4net.Config;
using OpenQA.Selenium;
using static CoreLayer.WebDriverFactory;

namespace TestLayer
{
    public abstract class BaseTest : IDisposable
    {
        public ILog Log
        {
            get { return LogManager.GetLogger(GetType()); }
        }
        public IWebDriver _driver;
        public MainPage _mainPage = null!;
        protected BaseTest()
        {
            XmlConfigurator.Configure(new FileInfo("Log.config"));
            var browserType = (BrowserType)Enum.Parse(typeof(BrowserType), Configuration.BrowserType);
            _driver = CreateWebDriver(browserType);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _driver.Quit();
            }
        }
    }
}
