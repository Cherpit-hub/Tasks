using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.PageObjects
{
    internal class SearchResultPage : BasePage
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
