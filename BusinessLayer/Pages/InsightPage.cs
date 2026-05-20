using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;

namespace BusinessLayer.PageObjects
{
    public class InsightPage : BasePage
    {
        readonly By _InsightArticleTitleLocator = By.XPath("//div[starts-with(@class,'owl-item')][contains(@class,'active')]//span[contains(@class,'60')]//span[string-length (text())>0]");
        readonly By _InsighArticleReadMoreButtonLocator = By.XPath("//div[contains(@class,'media-content')]//div[starts-with(@class,'owl-item')][contains(@class,'active')]//a[contains(text(),'Read')]");
        readonly By _CarouselRightButtonLocator = By.CssSelector("div[class*= media-content] div[class *= slider__navigation]:not([class *= 'disabled'])>button[class *= 'right']");
        public InsightPage(IWebDriver driver) : base(driver)
        {
        }
        public ReadOnlyCollection<string> GetInsightArticles()
        {
            List<string> articleTitles = new List<string>();
            Log.Info("Retrieving insight article titles from the carousel.");
            foreach (var article in _driver.FindElements(_InsightArticleTitleLocator))
            {
                articleTitles.Add(article.Text);
            }
            Log.Info($"Retrieved {articleTitles.Count} article titles: {string.Join(", ", articleTitles)}");
            return articleTitles.AsReadOnly();
        }
        public void ClickCarouselRightButton()
        {
            Random random = new Random();
            var counter = random.Next(2, 5);
            Log.Info($"Clicking carousel right button {counter} times.");
            while (counter > 0) 
            {
                ScrollToElement(_CarouselRightButtonLocator);
                new Actions(_driver)
                    .Click(FindElement(_CarouselRightButtonLocator))
                    .Perform();
                counter--;
            }
            Log.Info("Finished clicking carousel right button.");
        }
        public InsightArticlePage ClickReadMoreButtonOfFirstInsightArticle()
        {
            Log.Info("Clicking 'Read More' button of the insight article.");
            ScrollToElement(_InsighArticleReadMoreButtonLocator);
            FindElement(_InsighArticleReadMoreButtonLocator).Click();
            Log.Info("'Read More' button clicked.");
            return new InsightArticlePage(_driver);
        }
    }
}
