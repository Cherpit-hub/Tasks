using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsForWebElements.BusinessLayer.PageObjects
{
    public class InsightPage : BasePage
    {
        readonly By _InsightArticleTitleLocator = By.XPath("//div[starts-with(@class,'owl-item')][contains(@class,'active')]//span[contains(@class,'60')]//span[string-length (text())>0]");
        readonly By _InsighArticleReadMoreButtonLocator = By.XPath("//div[starts-with(@class,'owl-item')][contains(@class,'active')]//a[contains(text(),'Read')]");
        readonly By _CarouselRightButtonLocator = By.CssSelector("div[class *= slider__navigation]:not([class *= 'disabled'])>button[class *= 'right']");
        public InsightPage(IWebDriver driver) : base(driver)
        {
        }
        public ReadOnlyCollection<string> GetInsightArticles()
        {
            List<string> articleTitles = new List<string>();
            foreach (var article in _driver.FindElements(_InsightArticleTitleLocator))
            {
                articleTitles.Add(article.Text);
            }
            return articleTitles.AsReadOnly();
        }
        public void ClickCarouselRightButton()
        {
            Random random = new Random();
            var counter = random.Next(2, 5);
            while (counter > 0) 
            {
                ScrollToElement(_CarouselRightButtonLocator);
                new Actions(_driver)
                    .Click(FindElement(_CarouselRightButtonLocator))
                    .Perform();
                counter--;
            }
        }
        public InsightArticlePage ClickReadMoreButtonOfFirstInsightArticle()
        {
            ScrollToElement(_InsighArticleReadMoreButtonLocator);
            FindElement(_InsighArticleReadMoreButtonLocator).Click();
            return new InsightArticlePage(_driver);
        }
    }
}
