using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsForWebElements.PageObjects
{
    internal class InsightArticlePage : BasePage
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
