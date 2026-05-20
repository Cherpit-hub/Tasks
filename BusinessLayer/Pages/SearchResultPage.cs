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
            Log.Info("Retrieving search results from the search result page.");
            var results = _driver.FindElements(_articleLocator);
            Log.Info($"Retrieved {results.Count} search results.");
            return results;
        }
    }
}
