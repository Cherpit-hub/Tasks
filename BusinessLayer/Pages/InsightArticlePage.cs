using OpenQA.Selenium;

namespace BusinessLayer.PageObjects
{
    public class InsightArticlePage : BasePage
    {
        readonly By _ArticleTitleLocator = By.CssSelector("div[class*= header] h1[class *= 'remove-heading-style']");
        public InsightArticlePage(IWebDriver driver) : base(driver)
        {
        }
        public string GetArticleTitle()
        {
            Log.Info("Retrieving article title.");
            string title = FindElement(_ArticleTitleLocator).Text;
            Log.Info($"Article title retrieved: {title}");
            return title;
        }
    }
}
