using OpenQA.Selenium;

namespace BusinessLayer.PageObjects
{
    public class InsightArticlePage : BasePage
    {
        readonly By _ArticleTitleLocator = By.CssSelector("h1[class *= 'remove-heading-style']");
        public InsightArticlePage(IWebDriver driver) : base(driver)
        {
        }
        public string GetArticleTitle()
        {
            return FindElement(_ArticleTitleLocator).Text;
        }
    }
}
