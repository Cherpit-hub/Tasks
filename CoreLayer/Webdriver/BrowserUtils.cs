using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Support.UI;
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

            public static bool IsFileDownloaded(string fileName, IWebDriver driver)
        {
            var downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            var filePath = Path.Combine(downloadPath, fileName);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            return wait.Until(d =>
            {
                try
                {
                    return File.Exists(filePath);
                }
                catch (Exception ex)
                {
                    if (ex is IOException || ex is UnauthorizedAccessException)
                    {
                        return false;
                    }
                    else throw;
                }
            });
        }
    }
}
