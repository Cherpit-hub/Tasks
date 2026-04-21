using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
namespace CoreLayer
{
    public static class BrowserUtils
    {
            public static string TakeBrowserScreenshot(IWebDriver driver)
            {
            var _driver = driver as ITakesScreenshot;
                var now = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff");
                var screenshotPath = Path.Combine(Environment.CurrentDirectory, $"Display_{now}.png");
                _driver!.GetScreenshot().SaveAsFile(screenshotPath);

                return screenshotPath;
            }
    }
}
