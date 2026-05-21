using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace CoreLayer.Webdriver
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(string browserType)
        {
            switch (browserType.ToLower())
            {
                case "chrome":
                    {
                        var service = ChromeDriverService.CreateDefaultService();
                        ChromeOptions options = new();
                        options.AddArgument("--incognito");
                        return new ChromeDriver(service, options);
                    }
                case "firefox":
                    {
                        var service = FirefoxDriverService.CreateDefaultService();
                        FirefoxOptions options = new();
                        options.AddArgument("--incognito");
                        return new FirefoxDriver(service, options);
                    }
                default:
                    throw new ArgumentException("Unknown browser type: " + browserType);
            }
        }
    }
}
