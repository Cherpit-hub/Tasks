using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace CoreLayer
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
                        options.AddArgument("--start-maximized");
                        options.AddArgument("--incognito");

                        return new ChromeDriver(service, options);
                    }
                case "firefox":
                    return new FirefoxDriver();
                default:
                    throw new ArgumentException("Unknown browser type: " + browserType);
            }
        }
    }
}
