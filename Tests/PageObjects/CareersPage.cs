using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.PageObjects
{
    internal class CareersPage : BasePage
    {
        readonly By startButtonLocator = By.ClassName("pinned-button-ui-23");
        public CareersPage(IWebDriver driver) : base(driver)
        {
        }
        public JobSearchPage ClickJobSearchPageButton()
        {
            FindElement(startButtonLocator).Click();
            return new JobSearchPage(_driver);
        }
    }
}
