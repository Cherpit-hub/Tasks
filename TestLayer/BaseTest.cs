using BusinessLayer.PageObjects;
using CoreLayer;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
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
                case "INFO":
                    root.Level = log4net.Core.Level.Info;
                    break;
                case "WARN":
                    root.Level = log4net.Core.Level.Warn;
                    break;
                case "ERROR":
                    root.Level = log4net.Core.Level.Error;
                    break;
                case "FATAL":
                    root.Level = log4net.Core.Level.Fatal;
                    break;
                case "ALL":
                    root.Level = log4net.Core.Level.All;
                    break;
                case "OFF":
                    root.Level = log4net.Core.Level.Off;
                    break;
                default:
                    throw new ArgumentException("Unknown log level: " + level);
            }
            ;
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
