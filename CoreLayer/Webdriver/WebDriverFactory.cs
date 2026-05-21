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
                        var options = new ChromeOptions();
                        options.AddArgument("--start-maximized");
                        options.AddArgument("--headless");
                        return new ChromeDriver(service, options);
                    }
                case "firefox":
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArgument("--start-maximized");
                    firefoxOptions.AddArgument("--headless");
                    firefoxOptions.BinaryLocation = "/usr/local/share/gecko_driver";
                    return new FirefoxDriver(firefoxOptions);
                default:
                    throw new ArgumentException("Unknown browser type: " + browserType);
            }
        }
    }
}
