//using BusinessLayer.PageObjects;
//using CoreLayer;
//using log4net;
//using log4net.Config;
//using log4net.Repository.Hierarchy;
//using OpenQA.Selenium;
//using static CoreLayer.WebDriverFactory;

//namespace TestLayer
//{
//    public abstract class BaseTest : IDisposable
//    {
//        public static ILog Log => CoreLayer.Logger.Log;
//        public IWebDriver _driver = null!;
//        public MainPage _mainPage = null!;
//        protected BaseTest()
//        {
//            XmlConfigurator.Configure(new FileInfo("Log.config"));
//        }

//        public void InitializeWebDriver()
//        {

//        }



//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//        protected virtual void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                _driver.Quit();
//            }
//        }
//    }
//}
