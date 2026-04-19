using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace BusinessLayer.PageObjects
{
    public class SearchResultPage : BasePage
    {
        readonly By _articleLocator = By.TagName("article");

        public SearchResultPage(IWebDriver driver) : base(driver)
        { }

        public ReadOnlyCollection<IWebElement> GetSearchResults()
        {
            return _driver.FindElements(_articleLocator);
        }
    }
}
